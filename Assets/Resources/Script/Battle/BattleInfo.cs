using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleInfo : MonoBehaviour {

	public static BattleInfo Instance;

	public List<string> m_PlayerSkillDeck;
	public List<string> m_AiSkillDeck;


	// Use this for initialization
	void Awake () {
		Instance = this;
		m_PlayerSkillDeck = new List<string> ();
		m_PlayerSkillDeck.Add ("SlashAttack");
		m_PlayerSkillDeck.Add ("SlashStance");
		m_PlayerSkillDeck.Add ("HitStance");

		m_AiSkillDeck = new List<string> ();
		m_AiSkillDeck.Add ("RiposteReaction");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
