using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IDamageable {
	[SerializeField]
	private float health = 15.0f;

	[SerializeField]
	private GameObject destroyedPrefab = null;

	[SerializeField]
	private float impactLifespan = 15.0f;

	private void Update() {
		if(health <= 0.0f) {
			var go = Instantiate(destroyedPrefab, transform.position, Quaternion.identity);
			Destroy(go, impactLifespan);
			Destroy(gameObject);
		}
	}

	public void ApplyDamage(float damage) {
		health -= damage;
	}
}
