﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

	[SerializeField]
	private Camera camera;

	[SerializeField]
	private float cameraRotationLimit = 85f;

	private Vector3 velocity = Vector3.zero;

	private Vector3 rotation = Vector3.zero;

	private float cameraRotationX = 0f;

	private float currentCameraRotationX = 0f;

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

	public void RotateCamera(float _cameraRotationX) {
		cameraRotationX = _cameraRotationX;
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
			currentCameraRotationX -= cameraRotationX;
			currentCameraRotationX = Mathf.Clamp (currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
			camera.transform.localEulerAngles = new Vector3 (currentCameraRotationX, 0f, 0f);
		}
	}


}
