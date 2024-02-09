using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupBase : MonoBehaviour {
	[SerializeField]
	protected GameObject pickupPrefab = null;

	[SerializeField]
	protected AudioClip pickupAudio = null;

	[SerializeField]
	[Range(0, 200)]
	protected float volume = 100.0f;

	[SerializeField]
	[Range(0.1f, 30.0f)]
	protected float pickupFXLifeTime = 10.0f;

	private void OnTriggerEnter(Collider other) {
		Pickup(other);
	}

	protected abstract void Pickup(Collider other);

	protected void AfterPickup() {
		if(pickupPrefab != null) {
			Instantiate(pickupPrefab, transform.position, Quaternion.identity).AddComponent<SelfDestruct>().lifeTime = pickupFXLifeTime;
		}

		Destroy(this.gameObject);

		if(pickupAudio != null) {
			AudioSource.PlayClipAtPoint(pickupAudio, this.transform.position, volume);
		}
	}
}