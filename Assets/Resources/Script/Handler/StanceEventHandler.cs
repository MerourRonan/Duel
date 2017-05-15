using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class StanceEventHandler : CardEventHandler,IPointerClickHandler {

	public void OnPointerClick(PointerEventData eventData)
	{
		if (m_IsInteractable) {
			PlayerController.Instance.m_Fighter.RemoveStance ();
			StartCoroutine(UIManager.Instance.MoveCardToDeck (m_CardUI,"Player"));
			Destroy (this);
		}

	}
}
