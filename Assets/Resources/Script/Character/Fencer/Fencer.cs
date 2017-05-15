using UnityEngine;
using System.Collections;

public class Fencer : Fighter {

	void Awake()
	{
		m_HeartPoints = 20;
		m_StaminaPoints = 10;
		m_MoveCost = 2;
		m_Speed = 5;
		m_FighterAnimator = transform.GetComponent<FighterAnimator> ();
	}
	void Start()
	{
		m_FighterAnimator.LoadAnimation ("Fencer");
	}
}
