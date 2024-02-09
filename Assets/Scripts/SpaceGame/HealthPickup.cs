using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PickupBase {
	[SerializeField]
	[Range(1, 100)]
	int healthValue = 10;

	protected override void Pickup(Collider other) {
		if(other.gameObject.TryGetComponent(out IHealable healable)) {
			healable.ApplyHealth(healthValue);

			AfterPickup();
		}
	}
}