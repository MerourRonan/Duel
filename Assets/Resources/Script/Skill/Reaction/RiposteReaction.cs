using UnityEngine;
using System.Collections;

public class RiposteReaction : Skill,Reaction {

	public int m_TriggerDistance;
	public string m_DamageType;
	public int m_Damage;
	public int m_Range;

	void Awake () {
		m_SkillName = "Slash Ripost";
		m_SkillType = "Reaction";
		m_StaminaUse = 2;
		m_TriggerDistance = 10;
		m_DamageType = "Slash";
		m_Damage = 4;

		m_Action = new ReactionAction ();
	}

	public override void ConfigureCard(CardUI card)
	{
		base.ConfigureCard (card);
		card.ConfigureRange (0);
		card.AddProperty (m_DamageType, m_Damage);
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
		Fighter attacker = BattleManager.Instance.m_Attacker;
		m_Fighter.PlayAttackAnimation ();
		attacker.PlayHitAnimation ();
	}

	public bool TriggerReaction(Action action)
	{
		int defenderPos = m_Fighter.m_TilePosition;
		int attackerPos = BattleManager.Instance.m_Attacker.m_TilePosition;
		if (action.m_Type == "Attack" && Mathf.Abs (attackerPos - defenderPos) <= m_TriggerDistance) {
			return true;
		} else {
			return false;
		}
	}
}
