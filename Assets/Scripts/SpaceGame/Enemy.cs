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
	private GameObject destroyedPrefab = null;

	public void ApplyDamage(float damage) {
		health -= damage;
		if(health <= 0) {
			scoreEvent?.RaiseEvent(points);
			if(destroyedPrefab != null) {
				Instantiate(destroyedPrefab, gameObject.transform.position, Quaternion.identity);
			}
			Destroy(gameObject);
		} else {
			if(hitPrefab != null) {
				Instantiate(hitPrefab, gameObject.transform.position, Quaternion.identity);
			}
		}
	}
}
