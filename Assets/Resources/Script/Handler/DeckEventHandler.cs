using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DeckEventHandler : CardEventHandler,IPointerClickHandler {




	public void OnPointerClick(PointerEventData eventData)
	{
		if (m_IsInteractable) {
			PlayerController.Instance.SetCardSelected (m_CardUI);
		}
	}


}
