using UnityEngine;
using System.Collections;

public class FighterAnimator : MonoBehaviour {

	public SpriteRenderer m_FighterSpriteRenderer;
	public Sprite m_CurrentIdle;
	public Sprite m_Idle;
	public Sprite m_Hit;
	public Sprite m_Slash;
	public Sprite m_Spike;
	public Sprite m_Parry;
	public Sprite m_Dodge;
	public Sprite m_Stance;


	public virtual void Awake()
	{
		m_FighterSpriteRenderer = transform.GetComponentInChildren<SpriteRenderer> ();
	}

	public virtual void LoadAnimation(string fighterType)
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
			case 2:
				m_Spike = sprite ;
				break;
			case 3:
				m_Parry = sprite ;
				break;
			case 4:
				m_Hit = sprite ;
				break;
			case 5:
				m_Dodge = sprite ;
				break;
			case 6:
				m_Stance = sprite ;
				break;
			}
			spriteNumber++;
		}
	}
	public void CurrentIdleAnimation()
	{
		m_FighterSpriteRenderer.sprite = m_CurrentIdle;
	}

	public void IdleAnimation()
	{
		m_FighterSpriteRenderer.sprite = m_Idle;
	}

	public void SlashAnimation()
	{
		m_FighterSpriteRenderer.sprite = m_Slash;
	}

	public void HitAnimation()
	{
		m_FighterSpriteRenderer.sprite = m_Hit;
	}
	public void ParryAnimation()
	{
		m_FighterSpriteRenderer.sprite = m_Parry;
	}
	public void StanceAnimation()
	{
		m_FighterSpriteRenderer.sprite = m_Stance;
		m_CurrentIdle = m_Stance;
	}

	public void ResetIdle()
	{
		m_CurrentIdle = m_Idle;
	}

}
