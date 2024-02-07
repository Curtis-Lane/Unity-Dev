using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShip : Enemy {
	[SerializeField]
	private WeaponBase weapon;

	[SerializeField]
	private float minFireRate;
	
	[SerializeField]
	private float maxFireRate;

	//[SerializeField]
	//private float detectionRadius = 5.0f;

	//[SerializeField]
	//private float detectionDistance = 50.0f;

	//[SerializeField]
	//private LayerMask playerLayerMask;

	// Start is called before the first frame update
	void Start() {
		weapon.Equip();

		StartCoroutine(FireTimerCR());
	}

	// Update is called once per frame
	void Update() {
		//Ray ray = new Ray(transform.position, transform.forward);
		//if(Physics.SphereCast(ray, 5, out RaycastHit raycastHit, 50, playerLayerMask)) {
		//	inventory.Use();
		//}
		//Debug.DrawRay(ray.origin, ray.direction * raycastHit.distance, Color.red);

		//if(health <= 0.0f) {
		//	var go = Instantiate(destroyedPrefab, transform.position, Quaternion.identity);
		//	Destroy(go, impactLifespan);
		//	Destroy(gameObject);
		//}
	}

	//private void OnDrawGizmos() {
	//	Ray ray = new Ray(transform.position, transform.forward);
	//	Gizmos.color = Color.red;
	//	Gizmos.DrawWireSphere(ray.origin + ray.direction * detectionDistance, detectionRadius);
	//}

	IEnumerator FireTimerCR() {
		while(true) {
			float time = Random.Range(minFireRate, maxFireRate);
			yield return new WaitForSeconds(time);
			weapon.Use();
		}
	}
}
