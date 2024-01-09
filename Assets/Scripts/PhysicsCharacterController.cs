using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsCharacterControllre : MonoBehaviour {
	[SerializeField]
	[Range(1, 10)]
	float maxForce = 5;

    [SerializeField]
    [Range(1, 10)]
    float jumpForce = 5;

	[SerializeField]
	Transform view;

    Rigidbody rb;
	Vector3 force = Vector3.zero;

	// Start is called before the first frame update
	void Start() {
        rb = GetComponent<Rigidbody>();
    }

	// Update is called once per frame
	void Update() {
		Vector3 direction = Vector3.zero;

		direction.x = Input.GetAxis("Horizontal");
		direction.z = Input.GetAxis("Vertical");

		force = view.rotation * direction * maxForce;

		if(Input.GetButtonDown("Jump")) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
	}

	void FixedUpdate() {
		rb.AddForce(force, ForceMode.Force);
	}
}
