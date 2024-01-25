using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesPickup : MonoBehaviour {
	[SerializeField]
	GameObject pickupPrefab = null;

	[SerializeField]
	AudioClip pickupAudio;

	[SerializeField]
	[Range(1, 3)]
	int lifeValue = 1;

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.TryGetComponent(out Player player)) {
			//player.AddLives(lifeValue);
			GameManager.Instance.Lives += lifeValue;
		}

		Instantiate(pickupPrefab, transform.position, Quaternion.identity).AddComponent<SelfDestruct>().lifeTime = 10.0f;
		Destroy(this.gameObject);
		AudioSource.PlayClipAtPoint(pickupAudio, this.transform.position);
	}
}
