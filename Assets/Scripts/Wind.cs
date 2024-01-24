using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour {
	[SerializeField]
	Transform direction;
	[SerializeField]
	ForceMode forceMode = ForceMode.Force;
	[SerializeField]
	float force = 1.0f;

	private void OnTriggerStay(Collider other) {
		if(other.gameObject.TryGetComponent(out Rigidbody rb)) {
			rb.AddForce(direction.up * force, forceMode);
		}
	}
}
