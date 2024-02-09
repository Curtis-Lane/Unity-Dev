using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Animations.Rigging;

public class MultiFireWeapon : WeaponBase {
	[SerializeField] WeaponData weaponData;
	[SerializeField] Animator animator;
	//[SerializeField] RigBuilder rigBuilder;
	[SerializeField] Transform[] ammoTransforms;

	private int ammoTransformIndex = 0;
	private int ammoCount = 0;
	private bool weaponReady = false;

	private IEnumerator autoFireCoroutine;

	private void Start() {
		autoFireCoroutine = AutoFire();
		if(ammoTransforms == null || ammoTransforms.Length == 0) {
			ammoTransforms = new Transform[1];
			ammoTransforms[0] = transform;
		}
	}

	public override ItemData GetData() {return weaponData;}

	public override void Equip() {
		base.Equip();
		weaponReady = true;
		if(weaponData.animEquipName != "")	
			animator.SetBool(weaponData.animEquipName, true);
		for(int i = 0; i < weaponData.rigLayerWeight.Length; i++) {
			//rigBuilder.layers[i].rig.weight = weaponData.rigLayerWeight[i];
		}
	}

	public override void Unequip() {
		base.Unequip();
		if(weaponData.animEquipName != "") {
			animator.SetBool(weaponData.animEquipName, false);
		}
	}

	public override void Use() {
		if(!weaponReady) {
			return;
		}

		// trigger weapon animation if trigger name set and animator exists
		// ammo will be created through animation event
		if(weaponData.animTriggerName != "" && animator != null) {
			animator.SetTrigger(weaponData.animTriggerName);
			weaponReady = false;
		} else {
			// create ammo prefab
			if(weaponData.usageType == UsageType.SINGLE || weaponData.usageType == UsageType.BURST) {
				Instantiate(weaponData.ammoPrefab, ammoTransforms[ammoTransformIndex].position, ammoTransforms[ammoTransformIndex].rotation);
				ammoTransformIndex = (ammoTransformIndex + 1) % ammoTransforms.Length;
				if(weaponData.fireRate > 0) {
					weaponReady = false;
					StartCoroutine(ResetFireTimer());
				}
			} else {
				StartCoroutine(autoFireCoroutine);
			}
		}
	}

	public override void StopUse() {
		if(weaponData.usageType == UsageType.SINGLE || weaponData.usageType == UsageType.BURST) {
			weaponReady = true;
		}
		StopCoroutine(autoFireCoroutine);

	}

	public override bool isReady() {
		// check if ammo exists or weapon does not have rounds
		return weaponReady && (ammoCount > 0 || weaponData.rounds == 0);
	}

	public override void OnAnimEventItemUse() {
		// create ammo prefab
		Instantiate(weaponData.ammoPrefab, ammoTransforms[ammoTransformIndex].position, ammoTransforms[ammoTransformIndex].rotation);
		ammoTransformIndex = (ammoTransformIndex + 1) % ammoTransforms.Length;
	}

	IEnumerator ResetFireTimer() {
		yield return new WaitForSeconds(weaponData.fireRate);
		weaponReady = true;
	}

	IEnumerator AutoFire() {
		while(true) {
			Instantiate(weaponData.ammoPrefab, ammoTransforms[ammoTransformIndex].position, ammoTransforms[ammoTransformIndex].rotation);
			ammoTransformIndex = (ammoTransformIndex + 1) % ammoTransforms.Length;
			yield return new WaitForSeconds(weaponData.fireRate);
		}
	}
}
