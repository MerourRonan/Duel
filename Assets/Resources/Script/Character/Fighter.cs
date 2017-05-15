using UnityEngine;
using System.Collections;

public class Fighter : MonoBehaviour {

	public FighterAnimator m_FighterAnimator;
	public FighterInfo m_Info;

	public int m_TilePosition;
	public string m_Team;

	public int m_HeartPoints;
	public int m_StaminaPoints;

	public int m_MoveCost;

	public int m_SlashBonus;
	public int m_EstocBonus;
	public int m_HitBonus;

	public int m_DodgePoints;
	public int m_ParryPoints;
	public int m_BlockPoints;

	public int m_DodgeRetaliation;
	public int m_ParryRetaliation;
	public int m_BlockRetaliation;

	public bool m_Vigilance;
	public bool m_Retaliation;
	public int m_RetaliationValue;

	public CardUI m_ReactionCard;
	public CardUI m_StanceCard;

	public Reaction m_Reaction;
	public Stance m_Stance;

	/*** Animation ***/
	public float m_Speed;

	void Awake()
	{
		m_HeartPoints = 20;
		m_StaminaPoints = 10;
		m_MoveCost = 2;
		m_Speed = 5;
		m_FighterAnimator = transform.GetComponent<FighterAnimator> ();
	}
	void Start()
	{
		m_FighterAnimator.LoadAnimation ("Fencer");
	}

	public void TurnReset()
	{
		m_StaminaPoints = 10;
	}

	public bool CheckReaction(Action action)
	{
		if (m_Reaction != null && m_Reaction.TriggerReaction (action)) {
			m_Reaction.ApplyReaction ();
			BattleManager.Instance.m_DefenderResetCards.Add (m_ReactionCard);
			m_Reaction = null;
			m_ReactionCard = null;
			return true;
		}
		return false;
	}

	public IEnumerator Move(int tileNumber, int staminaUse)
	{
		//Debug.Log (this.gameObject.name + " : Move");
		Vector3 destination = GameManager.Instance.m_Tiles [tileNumber].transform.position;
		while (transform.position != destination) {
			float step = m_Speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, destination, step);
			yield return new WaitForEndOfFrame ();
		}
		m_TilePosition = tileNumber;
		m_StaminaPoints -= staminaUse;
		//Debug.Log (this.gameObject.name + " : End Move");
	}
	public void SetReaction(Reaction reaction, CardUI reactionCard)
	{
		if (m_Reaction != null) {
			BattleManager.Instance.m_AttackerResetCards.Add (m_ReactionCard);
			m_Reaction = null;
			m_ReactionCard = null;
		}
		m_Reaction = reaction;
		m_ReactionCard = reactionCard;
	}

	public void SetStance(Stance stance, CardUI stanceCard)
	{
		if (m_Stance != null) {
			RemoveStance ();
		}
		m_Stance = stance;
		m_StanceCard = stanceCard;
	}

	public void RemoveStance()
	{
		BattleManager.Instance.m_AttackerResetCards.Add (m_StanceCard);
		m_Stance.RemoveStance ();
		m_Stance = null;
		m_StanceCard = null;
	}

	public void PlayAttackAnimation()
	{
		m_FighterAnimator.SlashAnimation ();
	}

	public void PlayHitAnimation()
	{
		m_FighterAnimator.HitAnimation ();
	}
	public void PlayParryAnimation()
	{
		m_FighterAnimator.ParryAnimation ();
	}
	public void PlayStanceAnimation()
	{
		m_FighterAnimator.StanceAnimation ();
	}
	public void PlayCurrentIdleAnimation()
	{
		m_FighterAnimator.CurrentIdleAnimation ();
	}
	public void PlayIdleAnimation()
	{
		m_FighterAnimator.IdleAnimation ();
	}

}
