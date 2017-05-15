using UnityEngine;
using System.Collections;

public class AttackAction : Action {

	public AttackAction(string categorie,int value)
	{
		m_Type = "Attack";
		m_Categorie = categorie;
		m_Value = value;
	}
}
