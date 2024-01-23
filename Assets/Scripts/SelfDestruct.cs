using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {
	public float lifeTime;

	// Start is called before the first frame update
	void Start() {
		//
	}

	// Update is called once per frame
	void Update() {
		this.lifeTime -= Time.deltaTime;
		if(this.lifeTime <= 0 ) {
			Destroy(this.gameObject);
		}
	}
}
