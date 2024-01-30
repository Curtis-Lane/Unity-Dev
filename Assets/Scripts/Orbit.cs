using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {
	[SerializeField]
	Transform target;

	[SerializeField]
	[Range(0.0f, 10.0f)]
	float speed = 1.5f;

	// Start is called before the first frame update
	void Start() {
		//
	}

	// Update is called once per frame
	void Update() {
		gameObject.transform.RotateAround(target.position, Vector3.up, speed);
	}
}
