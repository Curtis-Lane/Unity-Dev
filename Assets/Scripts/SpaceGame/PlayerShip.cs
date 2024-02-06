using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Interactable, IDamageable, IHealable {
	[SerializeField]
	private Action action;

	[SerializeField]
	private Inventory inventory;

	[SerializeField]
	private FloatVariable healthVariable;

	[SerializeField]
	private const float MAX_HEALTH = 100.0f;

	private void Start() {
		if(action != null) {
			action.onEnter += OnInteractStart;
			action.onStay += OnInteractActive;
		}
	}

	// Update is called once per frame
	void Update() {
		if(Input.GetButtonDown("Fire1")) {
			inventory.Use();
		}
		if(Input.GetButtonUp("Fire1")) {
			inventory.StopUse();
		}

		if(healthVariable.value <= 0.0f) {
			Destroy(gameObject);
			GetComponentInParent<SplineFollower>().speed = 0;
		}
	}

	public void ApplyDamage(float damage) {
		healthVariable.value -= damage;
	}

	public void ApplyHealth(float health) {
		healthVariable.value = Mathf.Min(healthVariable.value + health, MAX_HEALTH);
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