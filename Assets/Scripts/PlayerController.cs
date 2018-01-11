using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]
public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float speed = 5f;

	[SerializeField]
	private float lookSensitivity = 3f;

	[SerializeField]
	private float thrusterForce = 1300f;

	[Header("Spring settings:")]
	[SerializeField]
	private float jointSpring = 15f;
	[SerializeField]
	private float jointMaxForce = 40f; 

	private PlayerMotor motor;

	private ConfigurableJoint joint;

	void Start() {
		motor = GetComponent<PlayerMotor> ();
		joint = GetComponent<ConfigurableJoint> ();

		SetJointSettings (jointSpring);
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

		float cameraRotationX = rotateHorizontal * lookSensitivity;
		motor.RotateCamera (cameraRotationX);

		// Calculate thruster force based on Player input
		Vector3 _thrusterForce = Vector3.zero; 
		if (Input.GetButton ("Jump")) {
			_thrusterForce = Vector3.up * thrusterForce;
			SetJointSettings (0f);
		} else {
			SetJointSettings (jointSpring);
		}

		motor.ApplyThruster (_thrusterForce);
	}

	private void SetJointSettings(float _jointSpring) {
		joint.yDrive = new JointDrive { 
			positionSpring = _jointSpring,
			maximumForce = jointMaxForce
		};
	}

}
