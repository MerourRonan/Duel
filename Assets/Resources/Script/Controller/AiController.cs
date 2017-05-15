using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AiController : FighterController {

	public static AiController Instance;

	public override void  Awake () {
		base.Awake ();
		Instance = this;
	}

	void Start()
	{
		

	}

	public void PlayReaction()
	{
		
		BattleManager.Instance.PlayCard (m_CardsDeck [0]);
		//GameManager.Instance.EndTurn ();
	}

	public IEnumerator AiPlay()
	{
		bool canplay = true;
		int fighterDistance = Mathf.Abs (m_Fighter.m_TilePosition - PlayerController.Instance.m_Fighter.m_TilePosition);
		int currentStamina = m_Fighter.m_StaminaPoints;
		while (canplay) {
			List<Score> playableCards =	ComputeUsefulCard (fighterDistance, currentStamina);
			if (playableCards.Count > 0) {
				CardUI bestCard = ComputeBestCard (playableCards);
				yield return StartCoroutine (BattleManager.Instance.AiPlayCard (bestCard));
			} else {
				
			}

		}
	}

	public void ComputeMove()
	{
		int currentTile = m_Fighter.m_TilePosition;
	}

	public List<Score> ComputeUsefulCard(int distance, int stamina)
	{
		List<Score> cardsPlayableScore = new List<Score> ();
		foreach (CardUI card in m_CardsDeck) {
			if (card.m_Skill.IsUseful(distance,stamina)) {
				cardsPlayableScore.Add (card.m_Skill.m_Score);
			}
		}
		return cardsPlayableScore;
	}

	public CardUI ComputeBestCard(List<Score> listScores)
	{
		Score scoreMax = ComputeScoreMax (listScores);
		CardUI bestCard = null;
		float bestCardPoint = 0;
		foreach (Score currentScore in listScores) {
			float currentCardPoint = 0;
			currentCardPoint += currentScore.m_Bonus / scoreMax.m_Bonus;
			currentCardPoint += currentScore.m_Damage / scoreMax.m_Damage;
			currentCardPoint += currentScore.m_Movement / scoreMax.m_Movement;
			currentCardPoint +=  scoreMax.m_Cost/currentScore.m_Cost ;
			if (currentCardPoint > bestCardPoint) {
				bestCardPoint = currentCardPoint;
				bestCard = currentScore.m_Card;
			}
		}
		return bestCard;
	}

	protected Score ComputeScoreMax(List<Score> listScores)
	{
		Score scoreMax = new Score ();
		foreach (Score score in listScores) {
			if (score.m_Bonus > scoreMax.m_Bonus) {
				scoreMax.m_Bonus = score.m_Bonus;
			}
			if (score.m_Cost > scoreMax.m_Cost) {
				scoreMax.m_Cost = score.m_Cost;
			}
			if (score.m_Damage > scoreMax.m_Damage) {
				scoreMax.m_Damage = score.m_Damage;
			}
			if (score.m_Movement > scoreMax.m_Movement) {
				scoreMax.m_Movement = score.m_Movement;
			}
		}
		return scoreMax;
	}



}
