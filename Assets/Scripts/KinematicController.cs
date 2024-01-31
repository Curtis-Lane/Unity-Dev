using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicController : MonoBehaviour, IDamageable {
	[SerializeField, Range(1, 40)]
	float speed = 1.0f;

	[SerializeField]
	float maxDistance;

	[SerializeField]
	float health = 100.0f;

	// Update is called once per frame
	void Update() {
		Vector3 direction = Vector3.zero;
		direction.x = Input.GetAxis("Horizontal");
		direction.y = Input.GetAxis("Vertical");

		Vector3 force = direction * speed * Time.deltaTime;
		transform.localPosition += force;

		transform.localPosition = Vector3.ClampMagnitude(transform.localPosition, maxDistance);
	}

	public void ApplyDamage(float damage) {
		health -= damage;
	}
}
