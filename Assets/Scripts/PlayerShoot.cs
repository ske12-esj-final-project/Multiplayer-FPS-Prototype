﻿using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

	private const string PLAYER_TAG = "Player";

	public PlayerWeapon weapon;

	[SerializeField]
	private Camera camera;

	[SerializeField]
	private LayerMask mask;

	void Start() {
		if (camera == null) {
			Debug.LogError ("PlayerShoot: no camera referenced!");
			this.enabled = false;
		}
	}

	void Update() {
		if (Input.GetButtonDown ("Fire1")) {
			Shoot ();
		}
	}

	[Client]
	void Shoot() {
		RaycastHit _hit;
		if (Physics.Raycast(camera.transform.position, camera.transform.forward, out _hit, weapon.range, mask)) {
			Debug.Log ("We hit " + _hit.collider.name);
		}

		if (_hit.collider.tag == PLAYER_TAG) {
			CmdPlayerShot (_hit.collider.name);
		}
	}

	[Command]
	void CmdPlayerShot(string _ID) {
		Debug.Log (_ID + " has been shot.");
	}
}
