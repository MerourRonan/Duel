using UnityEngine;
using System.Collections;

public class SlashAttack : Skill {

	public string m_DamageType;
	public int m_Damage;
	public int m_Range;
	public string m_ComboType;

	void Awake () {
		m_SkillName = "Slash";
		m_SkillType = "Attack";
		m_StaminaUse = 2;

		m_DamageType = "Slash";
		m_Damage = 4;
		m_Range = 4;
		m_ComboType = "Estoc";

		m_Action = new AttackAction ("Slash", m_Damage);
	}

	public override void ConfigureCard(CardUI card)
	{
		base.ConfigureCard (card);
		card.ConfigureRange (m_Range);
		card.AddProperty (m_DamageType, m_Damage);
		card.AddProperty ("Combo : ", m_ComboType);
	}

	public override bool CanBeUsed()
	{
		int pos1 = m_Fighter.m_TilePosition;
		int pos2 = AiController.Instance.m_Fighter.m_TilePosition;

		if (m_Fighter.m_StaminaPoints<m_StaminaUse) {
			return false;
		}
		else if (Mathf.Abs (pos1-pos2) == m_Range)
			return true;
		else
			return false;
	}
	public override bool CanBeUsed(int distance,int stamina)
	{
		int pos1 = m_Fighter.m_TilePosition;
		int pos2 = AiController.Instance.m_Fighter.m_TilePosition;

		if (m_Fighter.m_StaminaPoints<m_StaminaUse) {
			return false;
		}
		else if (Mathf.Abs (pos1-pos2) == m_Range)
			return true;
		else
			return false;
	}

	public override void UseSkill()
	{
		Fighter attacker = BattleManager.Instance.m_Attacker;
		Fighter defender = BattleManager.Instance.m_Defender;
		attacker.m_StaminaPoints -= m_StaminaUse;
		attacker.PlayAttackAnimation ();
		defender.PlayHitAnimation ();
		UIManager.Instance.SpawnDamages (m_Damage, defender);

		//test
		attacker.m_ParryPoints+=2;

	}
}
