using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FighterController : MonoBehaviour {

	public Fighter m_Fighter;
	public List<CardUI> m_CardsDeck;

	// Use this for initialization
	public virtual void Awake () {
		m_CardsDeck = new List<CardUI> ();
	}


	public virtual void TurnReset()
	{
		m_Fighter.TurnReset ();
		DeckReset ();
	}

	public virtual void DeckReset()
	{
		foreach (CardUI card in m_CardsDeck) {
			card.TurnReset ();
		}
	}

	public void SetFighter(Fighter fighter)
	{
		m_Fighter = fighter;
	}
}
