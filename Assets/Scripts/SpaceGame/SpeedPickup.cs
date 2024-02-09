using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : PickupBase {
	[SerializeField]
	[Range(0, 50)]
	float speedValue = 2.5f;

	protected override void Pickup(Collider other) {
		if(other.gameObject.TryGetComponent(out PlayerShip playerShip)) {
			playerShip.AddSpeed(speedValue);

			AfterPickup();
		}
	}
}