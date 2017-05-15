using UnityEngine;
using System.Collections;

public class CardEventHandler : MonoBehaviour {

	public CardUI m_CardUI;
	public bool m_IsInteractable;


	public virtual void Awake () {
		m_CardUI = transform.GetComponent<CardUI> ();
		m_CardUI.m_CardEventHandler = this;
	}
	public virtual void Start () {
		CheckIsInteractable ();
	}
	public void CheckIsInteractable()
	{
		bool interactable = true;
		if(m_CardUI.m_IsPlayed || !m_CardUI.m_IsPlayable)
		{
			interactable= false;
		}
		m_IsInteractable = interactable;
		m_CardUI.m_CardAnimator.CardPlayable (interactable);
	}
}
