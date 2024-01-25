using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : Singleton<GameManager> {
	[SerializeField]
	GameObject titleUI;

	[SerializeField]
	GameObject gameUI;

    [SerializeField]
    GameObject winLoseUI;

    [SerializeField]
	TMP_Text livesUI;

	[SerializeField]
	TMP_Text timerUI;

    [SerializeField]
    TMP_Text winLoseText;

    [SerializeField]
	Slider healthUI;

	[SerializeField]
	FloatVariable health;

	[SerializeField]
	IntVariable score;

	[SerializeField]
	GameObject[] levelRespawnPoints;
	int levelIndex = 0;

	[SerializeField]
	int startLevelIndex = 0;

	[SerializeField]
	AudioSource audioPlayer;

	[SerializeField]
	AudioSource bgmPlayer;

	[SerializeField]
	AudioClip winSoundFX;

	[SerializeField]
	AudioClip loseSoundFX;

	[Header("Events")]

	//[SerializeField]
	//IntEvent scoreEvent;

	[SerializeField]
	VoidEvent gameStartEvent;

    [SerializeField]
    VoidEvent gameEndEvent;

    [SerializeField]
	GameObjectEvent respawnEvent;

	[Header("Game Variables")]

	[SerializeField]
	int[] scoreToReach = {70, 40};

	public enum State {
		TITLE,
		START_GAME,
		PLAY_GAME,
		PLAYER_DEAD,
		GAME_WON,
		GAME_OVER
	}

	public State state = State.TITLE;
	public float timer = 0.0f;
	public int lives = 0;

	public int Lives {get {return lives;} set{lives = value; livesUI.text = "Lives: " + lives.ToString();}}
	public float Timer {get {return timer;} set{timer = value; timerUI.text = string.Format("{0:F1}", Math.Max(0, timer));}}

	private float winLoseTimer = 0.0f;

	private void OnEnable() {
		//scoreEvent.Subscribe(OnAddPoints);
	}

	private void OnDisable() {
		//scoreEvent.Unsubscribe(OnAddPoints);
	}

	// Start is called before the first frame update
	void Start() {
		//scoreEvent.Subscribe(OnAddPoints);
		levelIndex = startLevelIndex;
		Lives = 3;
	}

	// Update is called once per frame
	void Update() {
		if(Input.GetButton("Cancel")) {
			if(!Application.isEditor) {
				Application.Quit();
				return;
			} else {
				print("Quit!");
			}
		}

		switch(state) {
			case State.TITLE:
				titleUI.SetActive(true);
				gameUI.SetActive(false);
				winLoseUI.SetActive(false);

				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				break;
			case State.START_GAME:
				titleUI.SetActive(false);
				gameUI.SetActive(true);
				winLoseUI.SetActive(false);

				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;

				Timer = 60.0f;
				winLoseTimer = 5.0f;
				//Lives = 3;
				health.value = 100.0f;
				score.value = 0;

				audioPlayer.clip = null;

				gameStartEvent.RaiseEvent();
				respawnEvent.RaiseEvent(levelRespawnPoints[levelIndex]);

				state = State.PLAY_GAME;
				break;
			case State.PLAY_GAME:
				Timer -= Time.deltaTime;
				if(Timer <= 0.0f) {
					state = State.PLAYER_DEAD;
					break;
				}

				if(score.value == scoreToReach[levelIndex]) {
					state = State.GAME_WON;
				}

				break;
			case State.PLAYER_DEAD:
				Lives -= 1;
				health.value = 100.0f;

				respawnEvent.RaiseEvent(levelRespawnPoints[levelIndex]);

				if(Lives != 0) {
					state = State.PLAY_GAME;
					if(Timer <= 0) Timer = 60.0f;
				} else {
					state = State.GAME_OVER;
				}
				break;
			case State.GAME_WON:
                winLoseUI.SetActive(true);

				winLoseText.text = "You collected all the coins!";

				gameEndEvent.RaiseEvent();

				if(audioPlayer.clip == null) {
					audioPlayer.clip = winSoundFX;
					audioPlayer.Play();
				}

				winLoseTimer -= Time.deltaTime;
				if(winLoseTimer <= 0.0f) {
					state = State.START_GAME;

					NextLevel();
				}

				break;
			case State.GAME_OVER:
                winLoseUI.SetActive(true);

				winLoseText.text = (Timer <= 0.0f) ? "You ran out of time!" : "You ran out of lives!";

				gameEndEvent.RaiseEvent();

				if(audioPlayer.clip == null) {
					audioPlayer.clip = loseSoundFX;
					audioPlayer.Play();
					bgmPlayer.Stop();
				}

				winLoseTimer -= Time.deltaTime;
				if(winLoseTimer <= 0.0f) {
					state = State.TITLE;

					RestartGame();
				}

                break;
		}

		healthUI.value = health.value / 100.0f;
	}

	public void OnStartGame() {
		state = State.START_GAME;
	}

	public void OnPlayerDead() {
		state = State.PLAYER_DEAD;
	}

	public void OnAddPoints(int points) {
		print(points);
	}

	private void NextLevel() {
		if(levelIndex + 1 != levelRespawnPoints.Length) {
			levelIndex += 1;
		}

		//
    }

	private void RestartGame() {
		levelIndex = 0;
	}
}
