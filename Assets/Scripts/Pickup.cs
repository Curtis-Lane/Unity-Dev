using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
	[SerializeField]
	GameObject pickupPrefab = null;

	[SerializeField]
	[Range(1, 25)]
	int pointValue = 10;

	private void OnCollisionEnter(Collision collision) {
		print(collision.gameObject.name);
	}

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.TryGetComponent(out Player player)) {
			player.AddPoints(pointValue);
		}

		Instantiate(pickupPrefab, transform.position, Quaternion.identity);
		Destroy(this.gameObject);
	}
}
