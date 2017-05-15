using UnityEngine;
using System.Collections;

public class SwordManAnimator : FighterAnimator {

	public virtual void Awake()
	{
		m_FighterSpriteRenderer = transform.GetComponentInChildren<SpriteRenderer> ();

	}

	public override void LoadAnimation(string fighterType)
	{
		Sprite[] sprites = Resources.LoadAll<Sprite>("Sprite/Fighter/"+fighterType);
		int spriteNumber = 0;
		foreach (Sprite sprite in sprites) {
			switch (spriteNumber) {
			case 0:
				m_Idle = sprite;
				m_CurrentIdle = m_Idle;
				break;
			case 1:
				m_Slash = sprite ;
				break;
			case -1:
				m_Spike = sprite ;
				break;
			case 3:
				m_Parry = sprite ;
				break;
			case 2:
				m_Hit = sprite ;
				break;
			case 4:
				m_Dodge = sprite ;
				break;
			case 5:
				m_Stance = sprite ;
				break;
			}
			spriteNumber++;
		}
	}
}
