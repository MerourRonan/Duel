using UnityEngine;
using System.Collections;

public abstract class Skill : MonoBehaviour {

	public Fighter m_Fighter;
	public CardUI m_Card;
	public string m_SkillName;
	public string m_SkillType;
	public int m_StaminaUse;
	public Action m_Action;
	public Score m_Score;


	public abstract bool CanBeUsed ();
	public abstract bool CanBeUsed (int distance,int stamina);
	public abstract void UseSkill ();

	public virtual void ConfigureCard(CardUI card)
	{
		m_Card = card;
		card.ConfigureCard (m_SkillName, m_SkillType, m_StaminaUse);
	}
		
	public virtual bool IsUseful(int distance,int stamina)
	{
		if (!CanBeUsed (distance, stamina)) {
			return false;
		} else {
			return true;
		}
	}

	public virtual void ComputeScore()
	{
		m_Score = new Score ();
		m_Score.m_Cost = this.m_StaminaUse;
		m_Score.m_Card = this.m_Card;
	}


}
