using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardManager : MonoBehaviour {

	public static CardManager Instance;

	GameObject m_CardPrefab;
	Transform m_PlayerDeck;
	Transform m_AiDeck;



	// Use this for initialization
	void Awake () {
		Instance = this;
		m_PlayerDeck = GameObject.Find ("UI/PlayerPanel/Deck").transform;
		m_AiDeck = GameObject.Find ("UI/EnemyPanel/Deck").transform;
		m_CardPrefab = Resources.Load ("Prefab/Card/CardDummy") as GameObject;
	}
	
	public void SpawnCardPlayerDeck(string skillName)
	{
		GameObject cardInstance = Instantiate (m_CardPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		cardInstance.transform.SetParent (m_PlayerDeck,false);
		Skill skill = PlayerController.Instance.m_Fighter.transform.Find ("Skills").GetComponent (skillName) as Skill;
		skill.m_Fighter = PlayerController.Instance.m_Fighter;
		CardUI cardScript = cardInstance.GetComponent<CardUI> ();
		cardScript.SetSkill(skill);
		PlayerController.Instance.m_CardsDeck.Add (cardScript);
		//CardFactory.Instance.AddScript (cardInstance, skillName);
		//PlayerController.Instance.m_SkillDeck.Add (cardInstance.GetComponent<Skill> ());
	}

	public void SpawnCardAiDeck(string skillName)
	{
		GameObject cardInstance = Instantiate (m_CardPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		cardInstance.transform.SetParent (m_AiDeck,false);
		Skill skill = AiController.Instance.m_Fighter.transform.Find ("Skills").GetComponent (skillName) as Skill;
		skill.m_Fighter = AiController.Instance.m_Fighter;
		CardUI cardScript = cardInstance.GetComponent<CardUI> ();
		cardScript.SetSkill(skill);
		AiController.Instance.m_CardsDeck.Add (cardScript);
//		CardFactory.Instance.AddScript (cardInstance, skillName);
//		AiController.Instance.m_SkillDeck.Add (cardInstance.GetComponent<Skill> ());
	}


}
