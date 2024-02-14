using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerShip : Interactable, IDamageable, IHealable {
	[SerializeField]
	private SplineFollower splineFollower;

	[SerializeField]
	private TMP_Text scoreText;

	[SerializeField]
	private VoidEvent playerDeadEvent;

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

	[SerializeField]
	[InspectorReadOnly]
	bool gameStarted = false;

	private void Start() {
		healthVariable.value = MAX_HEALTH;
	}

	// Update is called once per frame
	void Update() {
		if(gameStarted) {
			if(Input.GetButtonDown("Fire1")) {
				inventory.Use();
			}
			if(Input.GetKeyDown(KeyCode.Tab)) {
				inventory.CycleItem();
			}
		}

		//if(Input.GetButtonUp("Fire1")) {
		//	inventory.StopUse();
		//}

		//splineFollower.speed = (Input.GetKey(KeyCode.Space)) ? 20 : 10;
	}

	public void ApplyDamage(float damage) {
		healthVariable.value -= damage;
		if(healthVariable <= 0.0f) {
			if(destroyedPrefab != null) {
				Instantiate(destroyedPrefab, gameObject.transform.position, Quaternion.identity);
			}
			if(destroyedSound != null && audioSource != null) {
				audioSource.clip = destroyedSound;
				audioSource.volume = destroyedSoundVolume;
				audioSource.Play();
			}

			Destroy(gameObject);
			playerDeadEvent.RaiseEvent();
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

	public void ApplyHealth(float health) {
		healthVariable.value = Mathf.Min(healthVariable + health, MAX_HEALTH);
	}

	public void AddPoints(int points) {
		scoreVariable.value += points;
		scoreText.text = "Score: " + scoreVariable.value.ToString();
	}

	public void AddSpeed(float speedIncrease) {
		splineFollower.IncreaseSpeed(speedIncrease);
	}

	public void OnGameStart() {
		gameStarted = true;
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