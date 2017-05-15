using UnityEngine;
using System.Collections;

public class RetreatReaction : Skill,Reaction {

	public int m_TriggerDistance;
	public int m_RetreatDistance;

	void Awake () {
		m_SkillName = "Reflex Retreat";
		m_SkillType = "Reaction";
		m_StaminaUse = 2;
		m_TriggerDistance = 2;
		m_RetreatDistance = 1;

		m_Action = new ReactionAction ();
	}

	public override void ConfigureCard(CardUI card)
	{
		base.ConfigureCard (card);
		card.ConfigureRange (0);
	}

	public override bool CanBeUsed()
	{
		if (m_Fighter.m_StaminaPoints<m_StaminaUse) {
			return false;
		}
		else
			return true;
	}
	public override bool CanBeUsed(int distance,int stamina)
	{
			return true;
	}

	public override void UseSkill()
	{
		m_Fighter.SetReaction (this, this.m_Card);
		m_Fighter.m_StaminaPoints -= m_StaminaUse;
		m_Fighter.PlayParryAnimation ();
	}

	public void ApplyReaction ()
	{
		int direction = BattleManager.Instance.m_Defender.m_TilePosition - BattleManager.Instance.m_Attacker.m_TilePosition;
		int tileDestination = direction / Mathf.Abs (direction) * m_RetreatDistance + BattleManager.Instance.m_Defender.m_TilePosition;
		if (tileDestination >= 0 && tileDestination < GameManager.Instance.m_Tiles.Length) {
			StartCoroutine(BattleManager.Instance.m_Defender.Move (tileDestination, 0));
		}
	}

	public bool TriggerReaction(Action action)
	{
		int pos1 = BattleManager.Instance.m_Defender.m_TilePosition;
		int pos2 = BattleManager.Instance.m_Attacker.m_TilePosition;
		if (action.m_Type == "Move" && Mathf.Abs (pos2 - pos1) <= m_TriggerDistance) {
			return true;
		} else {
			return false;
		}
	}

}
