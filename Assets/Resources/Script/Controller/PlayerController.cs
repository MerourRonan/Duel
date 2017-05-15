using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : FighterController {

	public static PlayerController Instance;

	public int m_NextTileNumber;
	public int m_PreviousTileNumber;
	public int m_MoveBackwardStamina ;
	public int m_MoveForwardStamina ;

	public CardUI m_CardSelected;

	// Use this for initialization
	public override void Awake () {
		base.Awake ();
		Instance = this;
	}

	public override void TurnReset()
	{
		base.TurnReset ();
		UpdatePlayerUI ();
	}


//	public IEnumerator MovePlayerLeft()
//	{
//		yield return StartCoroutine(this.m_Fighter.Move(m_PreviousTileNumber, m_MoveBackwardStamina));
//		UpdatePlayerUI ();
//	}
//
//	public IEnumerator MovePlayerRight()
//	{
//		yield return StartCoroutine(this.m_Fighter.Move(m_NextTileNumber,m_MoveForwardStamina));
//		UpdatePlayerUI ();
//	}
	public void MovePlayerLeft()
	{
		int playerPos = m_Fighter.m_TilePosition;
		int enemyPos = AiController.Instance.m_Fighter.m_TilePosition;
		int dir = (enemyPos - playerPos) / Mathf.Abs (enemyPos - playerPos);
		BattleManager.Instance.PlayMove (dir * (-1),m_Fighter.m_MoveCost);

	}

	public void MovePlayerRight()
	{
		int playerPos = m_Fighter.m_TilePosition;
		int enemyPos = AiController.Instance.m_Fighter.m_TilePosition;
		int dir = (enemyPos - playerPos) / Mathf.Abs (enemyPos - playerPos);
		BattleManager.Instance.PlayMove (dir * (1),m_Fighter.m_MoveCost);
	}

	public void PlayCard()
	{
		if (m_CardSelected != null) {
			m_CardSelected.m_IsPlayed = true;
			Destroy (m_CardSelected.gameObject.GetComponent<CardEventHandler> ());
			BattleManager.Instance.PlayCard (m_CardSelected);
			SetCardSelected (null);
		}

	}


	public void UpdatePlayerUI()
	{
		UpdateUIArrow ();
		UpdateUIStamina ();
		UpdateUIDeck ();
	}

	protected void UpdateUIArrow()
	{
		m_PreviousTileNumber = this.m_Fighter.m_TilePosition - 1;
		m_NextTileNumber = this.m_Fighter.m_TilePosition + 1;
		m_MoveBackwardStamina = 2;
		m_MoveForwardStamina = 2;

		if (m_PreviousTileNumber == AiController.Instance.m_Fighter.m_TilePosition) {
			m_PreviousTileNumber--;
			m_MoveBackwardStamina = 4;
		}
		if (m_NextTileNumber == AiController.Instance.m_Fighter.m_TilePosition) {
			m_NextTileNumber++;
			m_MoveForwardStamina = 4;
		}

		if (m_PreviousTileNumber < 0 || m_MoveBackwardStamina > this.m_Fighter.m_StaminaPoints) {
			UIManager.Instance.SetLeftArrow(false);
		} else {
			UIManager.Instance.SetLeftArrow(m_MoveBackwardStamina);
		}

		if (m_NextTileNumber > 8 || m_MoveForwardStamina > this.m_Fighter.m_StaminaPoints) {
			UIManager.Instance.SetRightArrow(false);
		} else {
			UIManager.Instance.SetRightArrow(m_MoveForwardStamina);
		}

	}
	protected void UpdateUIStamina()
	{
		UIManager.Instance.SetStaminaText (this.m_Fighter.m_StaminaPoints);
	}

	protected void UpdateUIDeck()
	{
		foreach (CardUI card in m_CardsDeck) {
			card.ResetCardState ();
		}
	}

	public void SetCardSelected(CardUI card)
	{
		if (card == m_CardSelected) {
			m_CardSelected.m_CardAnimator.IncreaserCardSize(false);
			m_CardSelected = null;
			return;
		}

		if (m_CardSelected != null) {
			m_CardSelected.m_CardAnimator.IncreaserCardSize(false);
		} 


		m_CardSelected = card;
		if (m_CardSelected != null) {
			m_CardSelected.m_CardAnimator.IncreaserCardSize(true);
		}
	}
}
