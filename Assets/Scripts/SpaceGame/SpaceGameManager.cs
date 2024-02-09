using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpaceGameManager : Singleton<SpaceGameManager> {
	//

	public enum State {
		TITLE,
		START_GAME,
		PLAY_GAME,
		PLAYER_DEAD,
		GAME_WON,
		GAME_OVER
	}

	public State state = State.TITLE;

	// Start is called before the first frame update
	void Start() {
		//
	}

	// Update is called once per frame
	void Update() {
		if(Input.GetButton("Cancel")) {
			if(!Application.isEditor) {
				Application.Quit();
			} else {
				EditorApplication.ExitPlaymode();
			}
			return;
		}
	}
}
