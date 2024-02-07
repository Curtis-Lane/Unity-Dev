using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Interactable, IDamageable, IHealable {
	[SerializeField]
	private Inventory inventory;

	[SerializeField]
	private IntVariable scoreVariable;

	[SerializeField]
	private FloatVariable healthVariable;

	[SerializeField]
	private const float MAX_HEALTH = 100.0f;

	[SerializeField]
	private GameObject hitPrefab = null;

	[SerializeField]
	private GameObject destroyedPrefab = null;

	private void Start() {
		healthVariable.value = MAX_HEALTH;
	}

	// Update is called once per frame
	void Update() {
		if(Input.GetButtonDown("Fire1")) {
			inventory.Use();
		}
		if(Input.GetButtonUp("Fire1")) {
			inventory.StopUse();
		}
	}

	public void ApplyDamage(float damage) {
		healthVariable.value -= damage;
		if(healthVariable <= 0.0f) {
			if(destroyedPrefab != null) {
				Instantiate(destroyedPrefab, gameObject.transform.position, Quaternion.identity);
			}
			Destroy(gameObject);
			GetComponentInParent<SplineFollower>().speed = 0;
		} else {
			if(hitPrefab != null) {
				Instantiate(hitPrefab, gameObject.transform.position, Quaternion.identity);
			}
		}
	}

	public void ApplyHealth(float health) {
		healthVariable.value = Mathf.Min(healthVariable.value + health, MAX_HEALTH);
	}

	public void AddPoints(int points) {
		scoreVariable.value += points;
	}

	public override void OnInteractStart(GameObject gameObject) {
		//
	}

	public override void OnInteractActive(GameObject gameObject) {
		//
	}

	public override void OnInteractEnd(GameObject gameObject) {
		//
	}
}