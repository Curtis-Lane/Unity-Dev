using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COR : MonoBehaviour {
	[SerializeField]
	float time = 3.0f;

	[SerializeField]
	bool go = false;

	Coroutine timerCoroutine;

	void Start() {
		timerCoroutine = StartCoroutine(Timer(time));
		StartCoroutine("StoryTime");
		StartCoroutine(WaitAction());
	}

	void Update() {
		//time -= Time.deltaTime;
		//if(time <= 0) {
		//	time = 3.0f;
		//	print("AAAAAAAAAAAA");
		//}
	}

	IEnumerator Timer(float time) {
		while(true) {
			yield return new WaitForSeconds(time);

			print("Tick");
		}

		//yield return null;
	}

	IEnumerator StoryTime() {
		print("Hello");
		yield return new WaitForSeconds(2.0f);
		print("Welcome to the new world");
		yield return new WaitForSeconds(2.0f);
		print("Population: You");

		StopCoroutine(timerCoroutine);

		yield return null;
	}

	IEnumerator WaitAction() {
		yield return new WaitUntil(() => go);
		print("go");
		yield return null;
	}
}
