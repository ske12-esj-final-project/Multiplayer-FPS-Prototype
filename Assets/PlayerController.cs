using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float speed = 5f;
	[SerializeField]
	private float lookSensitivity = 3f;

	private PlayerMotor motor;

	void Start() {
		motor = GetComponent<PlayerMotor> ();
	}

	void Update() {
		// Calculate movement velocity as a 3D Vector
		float moveHorizontal = Input.GetAxisRaw ("Horizontal"); 
		float moveVertical = Input.GetAxisRaw ("Vertical");

		Vector3 velocity = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized * speed;
		motor.Move (velocity);

		// Calculate rotation as a 3D Vector
		float rotateVertical = Input.GetAxisRaw("Mouse X");

		Vector3 rotation = new Vector3 (0.0f, rotateVertical, 0.0f) * lookSensitivity;
		motor.Rotate (rotation);

		// Calculate camera rotation as a 3D Vector
		float rotateHorizontal = Input.GetAxisRaw("Mouse Y");

		Vector3 cameraRotation = new Vector3 (rotateHorizontal, 0.0f, 0.0f) * lookSensitivity;
		motor.RotateCamera (cameraRotation);

	}

}
