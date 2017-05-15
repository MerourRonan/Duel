using UnityEngine;
using System.Collections;

public class CardFactory : MonoBehaviour {

	public static CardFactory Instance;
	// Use this for initialization
	void Awake () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddScript(GameObject card, string scriptName)
	{
		switch (scriptName) 
		{
		case "SlashAttack":
			card.AddComponent<SlashAttack> ();
			break;
		case "SlashStance":
			card.AddComponent<SlashStance> ();
			break;
		case "RetreatReaction":
			card.AddComponent<RetreatReaction> ();
			break;
		default:
			Debug.LogError ("Wrong card type : " + scriptName);
			break;
		}
	}
}
