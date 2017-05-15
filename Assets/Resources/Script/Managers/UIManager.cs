using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager Instance;

	public GameObject m_DamagePrefab;

	public GameObject PlayerPanel;
	public GameObject EnemyPanel;
	public GameObject EndTurn;

	public Transform m_ActionSlot;

	public Transform m_PlayerDeck;
	public Transform m_PlayerStanceSlot;
	public Transform m_PlayerReactionSlot;
	public Transform m_PlayerParry;
	public Transform m_PlayerBlock;
	public Transform m_PlayerDodge;
	public Transform m_PlayerLife;

	public Transform m_AiDeck;
	public Transform m_AiStanceSlot;
	public Transform m_AiReactionSlot;
	public Transform m_AiParry;
	public Transform m_AiBlock;
	public Transform m_AiDodge;
	public Transform m_AiLife;

	public Text m_StaminaText;

	public MoveArrow m_ArrowLeft;
	public MoveArrow m_ArrowRight;

	// Use this for initialization
	void Awake () {
		InitScript ();
	}

	void InitScript()
	{
		Instance = this;

		m_DamagePrefab = Resources.Load ("Prefab/UI/Damages") as GameObject;

		PlayerPanel = GameObject.Find ("UI/PlayerPanel");
		EnemyPanel = GameObject.Find ("UI/EnemyPanel");
		EndTurn = GameObject.Find ("UI/EndTurn");

		m_ActionSlot = GameObject.Find ("UI/ActionSlot").transform;

		m_PlayerDeck  =GameObject.Find ("UI/PlayerPanel/Deck").transform;
		m_PlayerStanceSlot  =GameObject.Find ("UI/PlayerPanel/StanceCard").transform;
		m_PlayerReactionSlot  =GameObject.Find ("UI/PlayerPanel/ReactionCard").transform;
		m_PlayerParry  =GameObject.Find ("UI/PlayerPanel/FighterInfo/Parry").transform;
		m_PlayerBlock  =GameObject.Find ("UI/PlayerPanel/FighterInfo/Block").transform;
		m_PlayerDodge  =GameObject.Find ("UI/PlayerPanel/FighterInfo/Dodge").transform;
		m_PlayerLife  =GameObject.Find ("UI/PlayerPanel/FighterLife").transform;

		m_AiDeck  =GameObject.Find ("UI/EnemyPanel/Deck").transform;
		m_AiStanceSlot  =GameObject.Find ("UI/EnemyPanel/StanceCard").transform;
		m_AiReactionSlot  =GameObject.Find ("UI/EnemyPanel/ReactionCard").transform;
		m_AiParry  =GameObject.Find ("UI/EnemyPanel/FighterInfo/Parry").transform;
		m_AiBlock  =GameObject.Find ("UI/EnemyPanel/FighterInfo/Block").transform;
		m_AiDodge  =GameObject.Find ("UI/EnemyPanel/FighterInfo/Dodge").transform;
		m_AiLife  =GameObject.Find ("UI/EnemyPanel/FighterLife").transform;

		m_StaminaText = GameObject.Find ("UI/ActionSlot/Stamina").GetComponent<Text> ();
		m_ArrowLeft = PlayerPanel.transform.Find ("MoveLeft").GetComponent<MoveArrow> ();
		m_ArrowRight = PlayerPanel.transform.Find ("MoveRight").GetComponent<MoveArrow> ();
	}

	public void SpawnDamages(int damages, Fighter fighter)
	{
		GameObject damageInstance = Instantiate (m_DamagePrefab, Vector3.zero, Quaternion.identity) as GameObject;
		damageInstance.transform.SetParent (EndTurn.transform.parent);
		damageInstance.transform.position  = Camera.main.WorldToScreenPoint(fighter.transform.position);
		damageInstance.GetComponentInChildren<Text> ().text = damages.ToString ();
	}

	public void UpdatePlayerFighterInfo()
	{
		Fighter fighter= PlayerController.Instance.m_Fighter;
		int parry = fighter.m_ParryPoints;
		int block = fighter.m_BlockPoints;
		int dodge = fighter.m_DodgePoints;
		if (parry == 0) {
			m_PlayerParry.gameObject.SetActive (false);
		} else {
			m_PlayerParry.gameObject.SetActive (true);
			m_PlayerParry.GetComponentInChildren<Text> ().text = parry.ToString ();
		}
		if (block == 0) {
			m_PlayerBlock.gameObject.SetActive (false);
		} else {
			m_PlayerBlock.gameObject.SetActive (true);
			m_PlayerBlock.GetComponentInChildren<Text> ().text = block.ToString ();
		}
		if (dodge == 0) {
			m_PlayerDodge.gameObject.SetActive (false);
		} else {
			m_PlayerDodge.gameObject.SetActive (true);
			m_PlayerDodge.GetComponentInChildren<Text> ().text = dodge.ToString ();
		}
	}

	public void UpdateAiFighterInfo()
	{
		Fighter fighter= AiController.Instance.m_Fighter;
		int parry = fighter.m_ParryPoints;
		int block = fighter.m_BlockPoints;
		int dodge = fighter.m_DodgePoints;
		if (parry == 0) {
			m_AiParry.gameObject.SetActive (false);
		} else {
			m_AiParry.gameObject.SetActive (true);
			m_AiParry.GetComponentInChildren<Text> ().text = parry.ToString ();
		}
		if (block == 0) {
			m_AiBlock.gameObject.SetActive (false);
		} else {
			m_AiBlock.gameObject.SetActive (true);
			m_AiBlock.GetComponentInChildren<Text> ().text = block.ToString ();
		}
		if (dodge == 0) {
			m_AiDodge.gameObject.SetActive (false);
		} else {
			m_AiDodge.gameObject.SetActive (true);
			m_AiDodge.GetComponentInChildren<Text> ().text = dodge.ToString ();
		}
	}

	public void UpdatePlayerLife()
	{
		m_PlayerLife.transform.position = Camera.main.WorldToScreenPoint(PlayerController.Instance.m_Fighter.transform.position);
		m_PlayerLife.GetComponentInChildren<Text> ().text = PlayerController.Instance.m_Fighter.m_HeartPoints.ToString();
	}
	public void UpdateAiLife()
	{
		m_AiLife.transform.position = Camera.main.WorldToScreenPoint(AiController.Instance.m_Fighter.transform.position);
		m_AiLife.GetComponentInChildren<Text> ().text = AiController.Instance.m_Fighter.m_HeartPoints.ToString();
	}



	protected void SpawnFighterInfo(Fighter fighter)
	{
		GameObject infoPrefab = Resources.Load ("Prefab/FighterInfo") as GameObject;
	}
	
	public void HideUiForBattle(bool active)
	{
		PlayerPanel.SetActive (!active);
		EnemyPanel.SetActive (!active);
		EndTurn.SetActive (!active);
	}

	public IEnumerator MoveCardToActionSlot(CardUI card)
	{
		card.transform.SetParent (m_ActionSlot);
		RectTransform cardRect = card.transform.GetComponent<RectTransform> ();
		cardRect.anchorMin = new Vector2 (0.5f, 0.5f);
		cardRect.anchorMax = new Vector2 (0.5f, 0.5f);
		cardRect.anchoredPosition3D = Vector3.zero;
		yield return true;
	}
	public IEnumerator MoveCardToDeck(CardUI card,string team)
	{
		Debug.Log (card.name);
		if (team == "Player") {
			card.transform.SetParent (m_PlayerDeck);
			card.m_IsPlayed = true;
			card.gameObject.AddComponent<DeckEventHandler>();

		} else {
			card.transform.SetParent (m_AiDeck);
			card.m_IsPlayed = true;
		}
		yield return true;
	}
	public IEnumerator MoveCardToStanceSlot(CardUI card,string team)
	{
		if (team == "Player") {
			card.gameObject.AddComponent<StanceEventHandler>();
			card.transform.SetParent (m_PlayerStanceSlot);
		} else {
			card.transform.SetParent (m_AiStanceSlot);
		}

		card.m_IsPlayed = true;
		yield return true;
	}
	public IEnumerator MoveCardToReactionSlot(CardUI card,string team)
	{
		if (team == "Player") {
			card.gameObject.AddComponent<StanceEventHandler>();
			card.transform.SetParent (m_PlayerReactionSlot);
		} else {
			card.transform.SetParent (m_AiReactionSlot);
		}

		card.m_IsPlayed = true;
		yield return true;
	}

	public void SetStaminaText(int stamina)
	{
		m_StaminaText.text = stamina.ToString ();
	}

	public void SetLeftArrow(int stamina)
	{
		m_ArrowLeft.SetValues (stamina);
	}
	public void SetLeftArrow(bool active)
	{
		m_ArrowLeft.SetValues (active);
	}
	public void SetRightArrow(int stamina)
	{
		m_ArrowRight.SetValues (stamina);
	}
	public void SetRightArrow(bool active)
	{
		m_ArrowRight.SetValues (active);
	}
}
