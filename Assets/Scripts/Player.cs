using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable {
	[SerializeField]
	TMP_Text scoreText;
	[SerializeField]
	FloatVariable health;
	[SerializeField]
	IntVariable score;

    public int Score {
        get {
            return score.value;
        }
        set {
            score.value = value;
            scoreText.text = "Score: " + score.value.ToString();
            scoreEvent.RaiseEvent(score.value);
        }
    }

    [SerializeField]
	PhysicsCharacterController characterController;

	[Header("Events")]

	[SerializeField]
	IntEvent scoreEvent = default;

	[SerializeField]
	VoidEvent gameStartEvent = default;

	[SerializeField]
	VoidEvent playerDeadEvent = default;

	private void OnEnable() {
		gameStartEvent.Subscribe(OnStartGame);
	}

	private void OnDisable() {
		gameStartEvent.Unsubscribe(OnStartGame);
	}

	private void Start() {
		gameStartEvent.Subscribe(OnStartGame);
		health.value = 50.0f;
	}

	public void AddPoints(int points) {
		Score += points;
	}

	void OnStartGame() {
		characterController.enabled = true;
	}

    public void OnEndGame() {
        characterController.enabled = false;
		characterController.Reset();
    }

    public void ApplyDamage(float damage) {
		health.value -= damage;
		if(health.value <= 0) {
			playerDeadEvent.RaiseEvent();
		}
	}

	public void OnRespawn(GameObject respawn) {
		transform.position = respawn.transform.position;
		transform.rotation = respawn.transform.rotation;

		characterController.Reset();
	}
}
