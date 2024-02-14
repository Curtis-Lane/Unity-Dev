using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpaceGameManager : Singleton<SpaceGameManager> {
	[SerializeField]
	GameObject titleUI;

	[SerializeField]
	GameObject gameUI;

	[SerializeField]
	GameObject winLoseUI;

	[SerializeField]
	TMP_Text winLoseText;

	[SerializeField]
	Slider healthUI;

	[SerializeField]
	FloatVariable health;

	[SerializeField]
	IntVariable score;

	[SerializeField]
	AudioSource audioPlayer;

	[SerializeField]
	AudioSource bgmPlayer;

	[SerializeField]
	AudioClip winSoundFX;

	[SerializeField]
	[Range(0, 200)]
	float winSFXVolume = 100.0f;

	[SerializeField]
	AudioClip loseSoundFX;

	[SerializeField]
	[Range(0, 200)]
	float lostSFXVolume = 100.0f;

	[Header("Events")]

	[SerializeField]
	VoidEvent gameStartEvent;

	[SerializeField]
	VoidEvent gameEndEvent;

	public enum State {
		TITLE,
		START_GAME,
		PLAY_GAME,
		GAME_WON,
		GAME_OVER
	}

	public State state = State.TITLE;

	[SerializeField]
	float winLoseTimer = 20.0f;

	// Start is called before the first frame update
	void Start() {
		//
	}

	// Update is called once per frame
	void Update() {
		if(Application.isEditor && Input.GetKeyDown(KeyCode.P)) {
			if(Cursor.lockState == CursorLockMode.Locked) {
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			} else {
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = true;
			}
		}

		if(Input.GetButton("Cancel")) {
			ExitGame();
			return;
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

				health.value = 100.0f;
				score.value = 0;

				audioPlayer.clip = null;

				gameStartEvent.RaiseEvent();

				state = State.PLAY_GAME;
				break;
			case State.PLAY_GAME:
				//
				break;
			case State.GAME_WON:
				winLoseUI.SetActive(true);

				winLoseText.text = "You survived the asteroid and enemy onslaught!\nYour final score was " + score.value.ToString();

				gameEndEvent.RaiseEvent();

				if(audioPlayer.clip == null) {
					audioPlayer.clip = winSoundFX;
					audioPlayer.volume = winSFXVolume;
					audioPlayer.Play();
				}

				winLoseTimer -= Time.deltaTime;
				if(winLoseTimer <= 0.0f) {
					ExitGame();
				}

				break;
			case State.GAME_OVER:
				winLoseUI.SetActive(true);

				winLoseText.text = "You succumbed to the asteroid and enemy onslaught.\nYour final score was " + score.value.ToString();

				gameEndEvent.RaiseEvent();

				if(audioPlayer.clip == null) {
					audioPlayer.clip = loseSoundFX;
					audioPlayer.volume = lostSFXVolume;
					audioPlayer.Play();
					bgmPlayer.Stop();
				}

				winLoseTimer -= Time.deltaTime;
				if(winLoseTimer <= 0.0f) {
					ExitGame();
				}

				break;
		}

		healthUI.value = health.value / 100.0f;
	}

	public void OnStartGame() {
		state = State.START_GAME;
	}

	public void OnEndOfTrack() {
		gameEndEvent.RaiseEvent();
		state = State.GAME_WON;
	}

	public void OnPlayerDead() {
		state = State.GAME_OVER;
	}

	private void ExitGame() {
		if(!Application.isEditor) {
			Application.Quit();
		} else {
			print("Quit!");
			EditorApplication.ExitPlaymode();
		}
	}
}
