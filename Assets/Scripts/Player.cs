using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour {
	[SerializeField]
	TMP_Text scoreText;

	private float health = 100.0f;
	private int score = 0;

	public int Score {
		get {
			return score;
		} set {
			score = value;
			scoreText.text = score.ToString();
		}
	}

	public void AddPoints(int points) {
		Score += points;
	}
}
