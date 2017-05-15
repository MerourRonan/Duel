using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FighterInfo : MonoBehaviour {

	public Text m_HeartPoints;
	public Text m_ParryPoints;
	public Text m_BlockPoints;
	public Text m_DodgePoints;

	public Fighter m_Fighter;


	// Use this for initialization
	void Awake () {
		m_HeartPoints = transform.Find ("HpMarker/Text").GetComponent<Text> ();
		m_ParryPoints = transform.Find ("Parry/Text").GetComponent<Text> ();
		m_BlockPoints = transform.Find ("Block/Text").GetComponent<Text> ();
		m_DodgePoints = transform.Find ("Dodge/Text").GetComponent<Text> ();
	}
	
	public void UpdateInfo(int heartPoints,int parryPoints, int blockPoints, int dodgePoints)
	{
		UpdateHeartPoints (heartPoints);
		UpdateParryPoints (parryPoints);
		UpdateBlockPoints (blockPoints);
		UpdateDodgePoints (dodgePoints);
	}

	protected void UpdateHeartPoints(int heartPoints)
	{
		m_HeartPoints.text = heartPoints.ToString ();
	}
	protected void UpdateParryPoints(int parryPoints)
	{
		if (parryPoints == 0) {
			m_ParryPoints.transform.parent.gameObject.SetActive (false);
		} else {
			m_ParryPoints.transform.parent.gameObject.SetActive (true);
			m_ParryPoints.text = parryPoints.ToString ();
		}
	}
	protected void UpdateBlockPoints(int blockPoints)
	{
		if (blockPoints == 0) {
			m_BlockPoints.transform.parent.gameObject.SetActive (false);
		} else {
			m_BlockPoints.transform.parent.gameObject.SetActive (true);
			m_BlockPoints.text = blockPoints.ToString ();
		}
	}
	protected void UpdateDodgePoints(int dodgePoints)
	{
		if (dodgePoints == 0) {
			m_DodgePoints.transform.parent.gameObject.SetActive (false);
		} else {
			m_DodgePoints.transform.parent.gameObject.SetActive (true);
			m_DodgePoints.text = dodgePoints.ToString ();
		}
	}
}
