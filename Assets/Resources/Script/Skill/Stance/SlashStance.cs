using UnityEngine;
using System.Collections;

public class SlashStance : Skill,Stance {

	protected string m_Property1;
	protected int m_Value1;

	protected int m_StaminaDrain;

	void Awake () {
		m_SkillName = "Slash Stance";
		m_SkillType = "Stance";
		m_StaminaUse = 4;

		m_Property1 = "Bonus Slash";
		m_Value1 = 2;
		m_StaminaDrain = 2;
		m_Action = new StanceAction ();
	}
		
	public override void ConfigureCard(CardUI card)
	{
		base.ConfigureCard (card);
		card.ConfigureRange (0);
		card.AddProperty (m_Property1, m_Value1);
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
		m_Fighter.SetStance (this, this.m_Card);
		ApplyStance ();
	}

	public void ApplyStance ()
	{
		m_Fighter.m_SlashBonus += m_Value1;
		m_Fighter.m_StaminaPoints -= m_StaminaUse;
		m_Fighter.PlayStanceAnimation ();
	}
	public void RemoveStance ()
	{
		m_Fighter.m_SlashBonus -= m_Value1;
		m_Fighter.m_FighterAnimator.ResetIdle ();
	}
	public void DrainStamina ()
	{
		m_Fighter.m_StaminaPoints -= m_StaminaDrain;
	}
}
