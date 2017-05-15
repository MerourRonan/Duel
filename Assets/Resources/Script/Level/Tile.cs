using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public int m_Number;



	// Use this for initialization
	void Start () {
		InitScript ();
	}
	
	void InitScript()
	{
		GameManager.Instance.SetTiles (m_Number, this);
	}
}
