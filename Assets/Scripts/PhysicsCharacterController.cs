using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsCharacterController : MonoBehaviour {
	[Header("Movement")]

	[SerializeField]
	[Range(1, 10)]
	float maxForce = 5;

    [SerializeField]
    [Range(1, 10)]
    float jumpForce = 5;

	[SerializeField]
	Transform view;

	[Header("Collision")]

	[SerializeField]
	[Range(1, 5)]
	float rayLength = 1.0f;

	[SerializeField]
	LayerMask groundLayerMask;

	[SerializeField]
    Rigidbody rb;

	Vector3 force = Vector3.zero;

	// Start is called before the first frame update
	void Start() {
        //rb = GetComponent<Rigidbody>();
    }

	// Update is called once per frame
	void Update() {
		Vector3 direction = Vector3.zero;

		direction.x = Input.GetAxis("Horizontal");
		direction.z = Input.GetAxis("Vertical");

		Quaternion yRotation = Quaternion.AngleAxis(view.rotation.eulerAngles.y, Vector3.up);
		force = yRotation * direction * maxForce;

		Debug.DrawRay(transform.position, Vector3.down * rayLength, Color.red);
		if(Input.GetButtonDown("Jump") && OnGround()) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
	}

	void FixedUpdate() {
		rb.AddForce(force, ForceMode.Force);
	}

	private bool OnGround() {
		return Physics.Raycast(transform.position, Vector3.down, rayLength, groundLayerMask);
	}

    public void Reset() {
        rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
    }
}
