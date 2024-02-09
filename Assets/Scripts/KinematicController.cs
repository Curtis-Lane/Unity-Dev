using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicController : MonoBehaviour {
	[SerializeField, Range(1, 40)]
	float speed = 1.0f;

	[SerializeField]
	float maxDistance;

	[SerializeField, Range(0.0f, 90.0f)]
	float tiltScale = 10.0f;

	[SerializeField, Range(0.0f, 30.0f)]
	float tiltLerpSpeed = 5.0f;

	// Update is called once per frame
	void Update() {
		Vector3 direction = Vector3.zero;
		direction.x = Input.GetAxis("Horizontal");
		direction.y = Input.GetAxis("Vertical");

		Vector3 force = direction * speed * Time.deltaTime;
		transform.localPosition += force;

		transform.localPosition = Vector3.ClampMagnitude(transform.localPosition, maxDistance);

		Quaternion qYaw = Quaternion.AngleAxis(direction.x * tiltScale, transform.up);
		Quaternion qPitch = Quaternion.AngleAxis(direction.y * tiltScale, transform.right);
		Quaternion rotation = qYaw * qPitch;

		transform.localRotation = Quaternion.Lerp(transform.localRotation, rotation, tiltLerpSpeed * Time.deltaTime);
	}
}
