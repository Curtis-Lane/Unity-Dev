using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {
	[SerializeField] float rate = 1.0f;
	[SerializeField] float amplitude = 1.0f;

	float time = 0.0f;
	Vector3 origin = Vector3.zero;

	private void Start() {
		origin = transform.position;
	}

	void Update() {
		time += Time.deltaTime * rate;
		float wave = Mathf.Sin(time) * amplitude;

		transform.position = origin + (Vector3.up * wave);
	}
}
