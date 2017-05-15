using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;
	public int m_TurnNumber;
	public Tile[] m_Tiles;



	// Use this for initialization
	void Awake () {
		Instance = this;
		m_Tiles = new Tile[9];
	}

	void Start()
	{
		SpawnPlayerFighter ();
		SpawnEnemyFighter ();
		SpawnPlayerDeck ();
		SpawnAiDeck ();
		PlayerController.Instance.TurnReset ();
		AiController.Instance.TurnReset ();
		UIManager.Instance.UpdateAiFighterInfo ();
		UIManager.Instance.UpdatePlayerFighterInfo ();
		UIManager.Instance.UpdatePlayerLife ();
		UIManager.Instance.UpdateAiLife ();
		EndTurn ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetTiles(int number , Tile tile)
	{
		m_Tiles [number] = tile;
	}

	private void SpawnPlayerFighter()
	{
		GameObject fighterPrefab = Resources.Load ("Prefab/Fighter/Fencer") as GameObject;
		GameObject fighterInstance = Instantiate (fighterPrefab, m_Tiles [2].transform.position, Quaternion.identity)as GameObject;
		Fighter fighterScript = fighterInstance.GetComponent<Fighter> ();
		fighterInstance.transform.SetParent(GameObject.Find("Fighters").transform);
		fighterInstance.gameObject.name = "PlayerFighter";
		fighterInstance.transform.Find ("Sprite").GetComponent<SpriteRenderer> ().flipX = true;
		fighterScript.m_Team= "Player";
		fighterScript.m_TilePosition = 2;
		PlayerController.Instance.SetFighter(fighterScript);
	}

	private void SpawnEnemyFighter()
	{
		GameObject fighterPrefab = Resources.Load ("Prefab/Fighter/SwordMan") as GameObject;
		GameObject fighterInstance = Instantiate (fighterPrefab, m_Tiles [6].transform.position, Quaternion.identity) as GameObject;
		Fighter fighterScript = fighterInstance.GetComponent<Fighter> ();
		fighterInstance.transform.SetParent(GameObject.Find("Fighters").transform);
		fighterInstance.gameObject.name = "EnemyFighter";
		fighterScript.m_Team= "Enemy";
		fighterScript.m_TilePosition = 6;
		AiController.Instance.SetFighter(fighterScript);
	}

	private void SpawnPlayerDeck()
	{
		foreach (string skillName in BattleInfo.Instance.m_PlayerSkillDeck) {
			CardManager.Instance.SpawnCardPlayerDeck (skillName);
		}
	}
	private void SpawnAiDeck()
	{
		foreach (string skillName in BattleInfo.Instance.m_AiSkillDeck) {
			CardManager.Instance.SpawnCardAiDeck (skillName);
		}
	}

	public void EndTurn()
	{
		m_TurnNumber++;
		if (m_TurnNumber % 2 == 0) {
			AiController.Instance.TurnReset ();
			BattleManager.Instance.m_Attacker = AiController.Instance.m_Fighter;
			BattleManager.Instance.m_Defender = PlayerController.Instance.m_Fighter;
			AiController.Instance.PlayReaction ();
		} else {
			PlayerController.Instance.TurnReset ();
			BattleManager.Instance.m_Defender = AiController.Instance.m_Fighter;
			BattleManager.Instance.m_Attacker = PlayerController.Instance.m_Fighter;
		}
	}
}
