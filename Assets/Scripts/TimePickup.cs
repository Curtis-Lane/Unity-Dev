using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePickup : MonoBehaviour {
	[SerializeField]
	GameObject pickupPrefab = null;

	[SerializeField]
	[Range(1, 60)]
	float timeValue = 25.0f;

	private void OnCollisionEnter(Collision collision) {
		print(collision.gameObject.name);
	}

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.TryGetComponent(out Player player)) {
			//player.AddLives(lifeValue);
			GameManager.Instance.Timer += timeValue;
		}

		Instantiate(pickupPrefab, transform.position, Quaternion.identity).AddComponent<SelfDestruct>().lifeTime = 10.0f;
		Destroy(this.gameObject);
	}
}
