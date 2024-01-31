using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {
	[SerializeField]
	float damage = 1.0f;

	[SerializeField]
	bool oneTime = true;

	private void OnTriggerStay(Collider other) {
		if(!oneTime && other.gameObject.TryGetComponent(out IDamageable damageable)) {
			damageable.ApplyDamage(damage * Time.deltaTime);
		}
	}

	private void OnTriggerEnter(Collider other) {
		if(oneTime && other.gameObject.TryGetComponent(out IDamageable damageable)) {
			damageable.ApplyDamage(damage);
		}
	}
}

public interface IDamageable {
	void ApplyDamage(float damage);
}