using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour {
	[SerializeField]
	Transform target = null;

	[SerializeField]
	[Range(20, 90)]
	float defaultPitch = 40.0f;

    [SerializeField]
    [Range(2, 8)]
    float distance = 5.0f;

	[SerializeField]
	[Range(1, 5)]
	float sensitivity = 1;

	float yaw = 0.0f;
	float pitch = 0.0f;

    // Start is called before the first frame update
    void Start() {
		pitch = defaultPitch;
	}

	// Update is called once per frame
	void Update() {
		yaw += Input.GetAxis("Mouse X") * sensitivity;
		pitch += -Input.GetAxis("Mouse Y") * sensitivity;

		pitch = Math.Clamp(pitch, -90, 90);

		Quaternion qYaw = Quaternion.AngleAxis(yaw, Vector3.up);
		Quaternion qPitch = Quaternion.AngleAxis(pitch, Vector3.right);
		Quaternion rotation = qYaw * qPitch;

		transform.position = target.position + (rotation * Vector3.back * distance);
		transform.rotation = rotation;
	}
}
