using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager> {
	[SerializeField]
	GameObject titleUI;

	[SerializeField]
	TMP_Text livesUI;
    [SerializeField]
    TMP_Text timerUI;

    public enum State {
		TITLE,
		START_GAME,
		PLAY_GAME,
		GAME_OVER
	}

	public State state = State.TITLE;
	public float timer = 0.0f;
	public int lives = 0;

	public int Lives {get {return lives;} set{lives = value; livesUI.text = "LIVES: " + lives.ToString();}}
	public float Timer {get {return timer;} set{timer = value; timerUI.text = string.Format("{0:F1}", timer);}}

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		switch(state) {
			case State.TITLE:
				titleUI.SetActive(true);
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				break;
			case State.START_GAME:
				titleUI.SetActive(false);
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				Timer = 60.0f;
				Lives = 3;

				state = State.PLAY_GAME;
				break;
			case State.PLAY_GAME:
				Timer -= Time.deltaTime;
				if(Timer <= 0.0f) {
					state = State.GAME_OVER;
				}
				break;
			case State.GAME_OVER:
				break;
		}
	}

	public void OnStartGame() {
		state = State.START_GAME;
	}
}
