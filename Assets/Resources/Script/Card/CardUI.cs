using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CardUI : MonoBehaviour {

	Transform m_PropertiesParent;
	GameObject m_CardPropertiesPrefab;
	public Text m_CardName;
	public Text m_CardType;
	public Text m_Range;
	public Image m_CardTypeColor;
	public Text m_Stamina;

	public Skill m_Skill;
	public CardAnimator m_CardAnimator;
	public CardEventHandler m_CardEventHandler;
	public bool m_IsPlayed;
	public bool m_IsPlayable;


	//FooterColor
	Color m_AttackColor = new Color(255,128,128,255)/255;
	Color m_DefenceColor = new Color(94,168,255,255)/255;
	Color m_StanceColor = new Color(95,216,79,255)/255;
	Color m_ReactionColor = new Color(255,62,234,255)/255;

	// Use this for initialization
	void Awake () {
		m_PropertiesParent = transform.Find ("Body");
		m_CardPropertiesPrefab = Resources.Load ("Prefab/Card/CardProperty") as GameObject;
		m_CardName = transform.Find ("Header/CardName").GetComponent<Text> ();
		m_Stamina = transform.Find ("Header/Stamina").GetComponent<Text> ();
		m_CardType = transform.Find ("Footer/CardType").GetComponent<Text> ();
		m_CardTypeColor = transform.Find ("Footer").GetComponent<Image> ();
		m_Range = transform.Find ("Range/Range").GetComponent<Text> ();
		m_CardAnimator = transform.GetComponent<CardAnimator> ();
	}


	
	public void AddProperty(string propertyName, int value )
	{
		GameObject propertyInstance = Instantiate (m_CardPropertiesPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		propertyInstance.transform.SetParent (m_PropertiesParent,false);

		propertyInstance.transform.Find ("PropertyName").GetComponent<Text> ().text = propertyName;
		if (value != 0) {
			propertyInstance.transform.Find ("PropertyValue").GetComponent<Text> ().text = value.ToString();
		}
	}
	public void AddProperty(string propertyName, string value)
	{
		GameObject propertyInstance = Instantiate (m_CardPropertiesPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		propertyInstance.transform.SetParent (m_PropertiesParent,false);

		propertyInstance.transform.Find ("PropertyName").GetComponent<Text> ().text = propertyName;
		propertyInstance.transform.Find ("PropertyValue").GetComponent<Text> ().text = value;

	}

	public void ConfigureCard(string cardName, string cardType, int staminaUse)
	{
		m_CardName.text = cardName;
		m_Stamina.text = staminaUse.ToString ();
		ConfigureFooter (cardType);
	}

	public void ConfigureRange( int rangeMin, int rangeMax)
	{
		if (rangeMax == rangeMin) {
			m_Range.text = rangeMin.ToString ();
		} else {
			m_Range.text = rangeMin.ToString ()+"-"+rangeMax.ToString ();
		}
	}

	public void ConfigureRange(int range)
	{
		m_Range.text = range.ToString ();

	}

	protected void ConfigureFooter(string cardType)
	{
		
		m_CardType.text = cardType;
		if (cardType == "Attack") {
			m_CardTypeColor.color = m_AttackColor;
		}
		else if (cardType == "Defence") {
			m_CardTypeColor.color = m_DefenceColor;
		}
		else if (cardType == "Stance") {
			m_CardTypeColor.color = m_StanceColor;
		}
		else if (cardType == "Reaction") {
			m_CardTypeColor.color = m_ReactionColor;
		}

	}
	public void TurnReset()
	{
		m_IsPlayed = false;
		SetIsPlayable(m_Skill.CanBeUsed ());
	}

	public void ResetCardState()
	{
		SetIsPlayable(m_Skill.CanBeUsed ());
	}

	public void SetIsPlayable(bool active)
	{
		m_IsPlayable = active;
		m_CardAnimator.CardPlayable(active);
		m_CardEventHandler.CheckIsInteractable ();
	}
	public void SetSkill(Skill skill)
	{
		m_Skill = skill;
		m_Skill.ConfigureCard (this);
	}


}
