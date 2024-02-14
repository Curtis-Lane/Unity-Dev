using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable {
	[SerializeField]
	private float health = 50.0f;

	[SerializeField]
	private int points = 15;

	[SerializeField]
	private IntEvent scoreEvent;

	[SerializeField]
	private GameObject hitPrefab = null;

	[SerializeField]
	private AudioClip hitSound = null;

	[SerializeField]
	private float hitSoundVolume = 100.0f;

	[SerializeField]
	private GameObject destroyedPrefab = null;

	[SerializeField]
	private AudioClip destroyedSound = null;

	[SerializeField]
	private float destroyedSoundVolume = 100.0f;

	[SerializeField]
	private AudioSource audioSource = null;

	public void ApplyDamage(float damage) {
		health -= damage;
		if(health <= 0) {
			scoreEvent?.RaiseEvent(points);
			if(destroyedPrefab != null) {
				Instantiate(destroyedPrefab, gameObject.transform.position, Quaternion.identity);
			}

			GetComponent<MeshRenderer>().enabled = false;
			GetComponent<Collider>().enabled = false;
			Destroy(gameObject, (destroyedSound != null) ? destroyedSound.length + 0.25f : 0);

			if(destroyedSound != null && audioSource != null) {
				audioSource.clip = destroyedSound;
				audioSource.volume = destroyedSoundVolume;
				audioSource.Play();
			}
		} else {
			if(hitPrefab != null) {
				Instantiate(hitPrefab, gameObject.transform.position, Quaternion.identity);
			}
			if(hitSound != null && audioSource != null) {
				audioSource.clip = hitSound;
				audioSource.volume = hitSoundVolume;
				audioSource.Play();
			}
		}
	}
}