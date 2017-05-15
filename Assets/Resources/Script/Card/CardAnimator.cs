using UnityEngine;
using System.Collections;

public class CardAnimator : MonoBehaviour {

	public CanvasGroup m_CanvasGroup;

	// Use this for initialization
	void Awake () {
		m_CanvasGroup = transform.GetComponent<CanvasGroup> ();
	}

	public void IncreaserCardSize(bool active)
	{
		if (active)
		{
			transform.localScale = transform.localScale * 1.2f;
		}
		else {
			transform.localScale = transform.localScale / 1.2f;
		}
	}
	
	public void CardPlayable(bool active)
	{
		if (active) {
			m_CanvasGroup.alpha = 1;
		} else {
			m_CanvasGroup.alpha = 0.4f;
		}
	}
}
