using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileAmmo))]
public class ProjectileDamage : MonoBehaviour {
	[SerializeField]
	private ProjectileAmmo projAmmo;

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.TryGetComponent(out IDamageable damageable)) {
			//projAmmo.OnDamage(((MonoBehaviour) damageable).gameObject);
		}
	}
}
