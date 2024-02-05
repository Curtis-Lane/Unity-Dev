using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {
	[SerializeField]
	GameObject pickupPrefab = null;

	[SerializeField]
	AudioClip pickupAudio;

	[SerializeField]
	[Range(1, 100)]
	int healthValue = 1;

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.TryGetComponent(out IHealable healable)) {
			healable.ApplyHealth(healthValue);
		}

		Instantiate(pickupPrefab, transform.position, Quaternion.identity).AddComponent<SelfDestruct>().lifeTime = 10.0f;
		Destroy(this.gameObject);
		AudioSource.PlayClipAtPoint(pickupAudio, this.transform.position);
	}
}
