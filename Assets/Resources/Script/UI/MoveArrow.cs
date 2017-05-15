using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveArrow : MonoBehaviour {

	public Text m_StaminaText;
	private Button m_Button;

	// Use this for initialization
	void Awake () {
		m_StaminaText = transform.Find ("Stamina").GetComponent<Text> ();
		m_Button = transform.GetComponent<Button> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	public void MovePlayerLeft()
//	{
//		Debug.Log ("Move player : " + this.gameObject.name);
//		StartCoroutine(PlayerController.Instance.MovePlayerLeft());
//	}
//
//	public void MovePlayerRight()
//	{
//		Debug.Log ("Move player : " + this.gameObject.name);
//		StartCoroutine(PlayerController.Instance.MovePlayerRight());
//	}
	public void MovePlayerLeft()
	{
		Debug.Log ("Move player : " + this.gameObject.name);
		PlayerController.Instance.MovePlayerLeft();
	}

	public void MovePlayerRight()
	{
		Debug.Log ("Move player : " + this.gameObject.name);
		PlayerController.Instance.MovePlayerRight();
	}

	public void SetValues(int staminaUse)
	{
		m_Button.interactable = true;
		m_StaminaText.text = staminaUse.ToString ();
	}
	public void SetValues(bool active)
	{
		m_Button.interactable = active;
	}
}
