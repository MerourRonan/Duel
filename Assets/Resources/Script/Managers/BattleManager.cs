using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour {

	public static BattleManager Instance;

	public float m_ActionDuration=0.5f;
	public float m_ReactionDuration=0.5f;
	public float m_MoveDuration=0.2f;


	public CardUI m_CardToPlay;
	public List<CardUI> m_AttackerResetCards;
	public List<CardUI> m_DefenderResetCards;
	public Fighter m_Attacker;
	public Fighter m_Defender;

	public int m_AttackerFirstMove;
	public int m_AttackerFirstMoveCost;

	public int m_AttackerFinalMove;
	public int m_DefenderFinalMove;
	public int m_AttackerFinalMoveCost;
	public int m_DefenderFinalMoveCost;

	// Use this for initialization
	void Awake () {
		Instance = this;
		m_AttackerResetCards = new List<CardUI> ();
		m_DefenderResetCards = new List<CardUI> ();
	}

	public void PlayCard(CardUI card)
	{
		BattleReset ();
		m_CardToPlay = card;
		StartCoroutine( CardBattleRoutine ());
	}
	public void PlayMove(int attackerFirstMove, int staminaCost)
	{
		BattleReset ();
		m_AttackerFirstMove = attackerFirstMove;
		m_AttackerFirstMoveCost = staminaCost;
		StartCoroutine( MoveBattleRoutine ());
	}

	public IEnumerator AiPlayCard(CardUI card)
	{
		BattleReset ();
		m_CardToPlay = card;
		yield return StartCoroutine( CardBattleRoutine ());
	}
	public IEnumerator AiPlayMove(int attackerFirstMove, int staminaCost)
	{
		BattleReset ();
		m_AttackerFirstMove = attackerFirstMove;
		m_AttackerFirstMoveCost = staminaCost;
		yield return StartCoroutine( MoveBattleRoutine ());
	}

	protected IEnumerator CardBattleRoutine()
	{
		yield return StartCoroutine (BeginUIRoutine ());
		yield return StartCoroutine (CardActionRoutine ());
		yield return StartCoroutine (CardReactionRoutine ());
		yield return StartCoroutine (FinalMoveRoutine ());
		yield return StartCoroutine (EndUIRoutine ());
	}
	protected IEnumerator MoveBattleRoutine()
	{
		yield return StartCoroutine (BeginUIRoutine ());
		yield return StartCoroutine (MoveActionRoutine ());
		yield return StartCoroutine (MoveReactionRoutine ());
		yield return StartCoroutine (FinalMoveRoutine ());
		yield return StartCoroutine (EndUIRoutine ());
	}


	protected IEnumerator CardActionRoutine()
	{
			m_CardToPlay.m_Skill.UseSkill ();
		yield return new WaitForSeconds (m_ActionDuration);
	}
	protected IEnumerator MoveActionRoutine()
	{
		yield return StartCoroutine (m_Attacker.Move (m_Attacker.m_TilePosition + m_AttackerFirstMove,m_AttackerFirstMoveCost));
	}
	protected IEnumerator CardReactionRoutine()
	{
		if (m_Defender.CheckReaction (m_CardToPlay.m_Skill.m_Action)) {
			yield return new WaitForSeconds (m_ReactionDuration);
		}
	}
	protected IEnumerator MoveReactionRoutine()
	{
		if (m_Defender.CheckReaction (new MoveAction())) {
			yield return new WaitForSeconds (m_ReactionDuration);
		}
	}
	protected IEnumerator FinalMoveRoutine()
	{
		
		if (m_AttackerFinalMove != 0) {
			StartCoroutine (m_Attacker.Move (m_Attacker.m_TilePosition + m_AttackerFinalMove,m_AttackerFinalMoveCost));
		}
		if (m_DefenderFinalMove != 0) {
			StartCoroutine (m_Defender.Move (m_Defender.m_TilePosition + m_DefenderFinalMove,m_DefenderFinalMoveCost));
		}
		yield return new WaitForSeconds (m_MoveDuration);
	}
	protected IEnumerator BeginUIRoutine()
	{
		if (m_CardToPlay != null) {
			yield return StartCoroutine (UIManager.Instance.MoveCardToActionSlot (m_CardToPlay));
		}
		UIManager.Instance.HideUiForBattle (true);
	}
	protected IEnumerator EndUIRoutine()
	{
		UIManager.Instance.HideUiForBattle (false);
		UIManager.Instance.UpdateAiFighterInfo ();
		UIManager.Instance.UpdatePlayerFighterInfo ();
		UIManager.Instance.UpdatePlayerLife ();
		UIManager.Instance.UpdateAiLife ();
		if (m_CardToPlay != null) {
			if (m_CardToPlay.m_Skill.m_SkillType == "Stance") {
				yield return StartCoroutine (UIManager.Instance.MoveCardToStanceSlot (m_CardToPlay, m_Attacker.m_Team));
			} else if (m_CardToPlay.m_Skill.m_SkillType == "Reaction") {
				yield return StartCoroutine (UIManager.Instance.MoveCardToReactionSlot (m_CardToPlay, m_Attacker.m_Team));
			} else {
				yield return StartCoroutine (UIManager.Instance.MoveCardToDeck (m_CardToPlay, m_Attacker.m_Team));
			}
		}
		foreach (CardUI card in m_AttackerResetCards) {
				yield return StartCoroutine (UIManager.Instance.MoveCardToDeck (card, m_Attacker.m_Team));
		}
		foreach (CardUI card in m_DefenderResetCards) {
			Debug.Log ("reseting defender card");
				yield return StartCoroutine (UIManager.Instance.MoveCardToDeck (card,m_Defender.m_Team));
		}
		m_Attacker.PlayCurrentIdleAnimation ();
		m_Defender.PlayCurrentIdleAnimation ();
		PlayerController.Instance.UpdatePlayerUI ();



	}

	protected void BattleReset()
	{
		m_CardToPlay = null;
		m_AttackerResetCards.Clear ();
		m_DefenderResetCards.Clear ();
		m_AttackerFirstMove = 0;
		m_AttackerFirstMoveCost = 0;
		m_AttackerFinalMove = 0;
		m_AttackerFinalMoveCost = 0;
		m_DefenderFinalMove = 0;
		m_DefenderFinalMoveCost = 0;
	}

//	public IEnumerator BattleRoutine()
//	{
//		while (m_BattleTime > 0) {
//			m_BattleTime -= Time.deltaTime;
//			yield return new WaitForEndOfFrame ();
//		}
//		m_Attacker.PlayCurrentIdleAnimation ();
//		m_Defender.PlayCurrentIdleAnimation ();
//		UIManager.Instance.HideUiForBattle (false);
//		if (m_CardToPlay.m_Skill.m_SkillType == "Stance") {
//			m_CardToPlay.m_SlotType = "Stance";
//			StartCoroutine (UIManager.Instance.MoveCardToStanceSlot (m_CardToPlay,m_Attacker.m_Team));
//		} else {
//			StartCoroutine (UIManager.Instance.MoveCardToDeck (m_CardToPlay));
//		}
//		PlayerController.Instance.UpdatePlayerUI ();
//
//	}

}
