﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

	[SerializeField]
	private Camera camera;

	private Vector3 velocity = Vector3.zero;

	private Vector3 rotation = Vector3.zero;

	private Vector3 cameraRotation = Vector3.zero;

	private Vector3 thrusterForce = Vector3.zero;

	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody> ();
	}

	public void Move(Vector3 _velocity) {
		velocity = _velocity;
	}

	public void Rotate(Vector3 _rotation) {
		rotation = _rotation;
	}

	public void RotateCamera(Vector3 _cameraRotation) {
		cameraRotation = _cameraRotation;
	}

	public void ApplyThruster(Vector3 _thrusterForce) {
		thrusterForce = _thrusterForce;
	}

	void FixedUpdate() {
		PerformMovement ();
		PerformRotation ();
	}

	void PerformMovement() {
		if (velocity != Vector3.zero) {
			rb.MovePosition (rb.position + velocity * Time.fixedDeltaTime);
		}

		if (thrusterForce != Vector3.zero) {
			rb.AddForce (thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
		}
	}

	void PerformRotation() {
		rb.MoveRotation (rb.rotation * Quaternion.Euler (rotation));
		if (camera != null) {
			camera.transform.Rotate (-cameraRotation);
		}
	}


}
