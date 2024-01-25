using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PointPickup : MonoBehaviour {
	[SerializeField]
	GameObject pickupPrefab = null;

	[SerializeField]
	AudioClip pickupAudio;

	[SerializeField]
	[Range(1, 25)]
	int pointValue = 10;

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.TryGetComponent(out Player player)) {
			player.AddPoints(pointValue);
		}

		Instantiate(pickupPrefab, transform.position, Quaternion.identity);
		Destroy(this.gameObject);
		AudioSource.PlayClipAtPoint(pickupAudio, this.transform.position, 1.25f);
	}
}
