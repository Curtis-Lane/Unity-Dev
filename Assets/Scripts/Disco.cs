using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disco : MonoBehaviour {
	[SerializeField]
	Light discoLight;

	// Update is called once per frame
	void Update() {
		discoLight.color = Random.ColorHSV(0, 1, 1, 1, 1, 1, 1, 1);
	}
}
