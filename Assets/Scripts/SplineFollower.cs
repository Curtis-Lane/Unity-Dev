using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

public class SplineFollower : MonoBehaviour {
	[SerializeField]
	SplineContainer splineContainer;

	[SerializeField]
	VoidEvent endOfTrackEvent;

	[SerializeField]
	[Range(0.1f, 0.99999f)]
	private float endOfTrackThreshold = 0.9f;

	[SerializeField]
	[InspectorReadOnly]
	private bool endOfTrackEventFired = false;

	[Range(0, 40)]
	public float initialSpeed = 10.0f;

	[SerializeField]
	[InspectorReadOnly]
	private float speed = 0.0f;

	[SerializeField]
	[Range(0.0f, 1.0f)]
	float tDistance = 0.0f; // Distance along spline (0-1)

	//public float speed {get; set;}
	// Length in world coordinates
	public float length {get {return splineContainer.CalculateLength();}}
	// Distance in world coordinates
	public float distance {
		get {
			return tDistance * length;
		} set {
			tDistance = value / length;
			tDistance = Utilities.Wrap(tDistance, 0.0f, 1.0f);
		}
	}
	public float TDistance {get {return tDistance;}}

	void Start () {
		//speed = maxSpeed;
	}

	// Update is called once per frame
	void Update() {
		distance += speed * Time.deltaTime;
		UpdateTransform(math.frac(tDistance));

		if(tDistance >= endOfTrackThreshold && !endOfTrackEventFired) {
			endOfTrackEvent?.RaiseEvent();
			endOfTrackEventFired = true;
		}
	}

	void UpdateTransform(float t) {
		Vector3 position = splineContainer.EvaluatePosition(t);
		Vector3 up = splineContainer.EvaluateUpVector(t);
		Vector3 forward = Vector3.Normalize(splineContainer.EvaluateTangent(t));
		Vector3 right = Vector3.Cross(up, forward);

		transform.position = position;
		transform.rotation = Quaternion.LookRotation(forward, up);
	}

	public void IncreaseSpeed(float increase) {
		initialSpeed += increase;
		speed = initialSpeed;
	}

	public void StartMoving() {
		speed = initialSpeed;
	}

	public void StopMoving() {
		speed = 0.0f;
	}
}
