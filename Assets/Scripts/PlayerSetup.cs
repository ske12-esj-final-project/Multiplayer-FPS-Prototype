using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

	[SerializeField]
	Behaviour[] componentsToDisable;

	[SerializeField]
	string remoteLayerName = "RemotePlayer";

	private Camera sceneCamera;

	void Start() {
		if (!isLocalPlayer) {
			DisableComponents ();
			AssignRemotePlayer ();

		}

		else {
			sceneCamera = Camera.main;
			if (sceneCamera != null) {
				sceneCamera.gameObject.SetActive (false);
			}
		}

		RegisterPlayer ();
	}

	void RegisterPlayer() {
		string _ID = "Player " + GetComponent<NetworkIdentity> ().netId;
		transform.name = _ID;
	}

	void DisableComponents() {
		for (int i = 0; i < componentsToDisable.Length; i++) {
			componentsToDisable [i].enabled = false;
		}
	}

	void AssignRemotePlayer() {
		gameObject.layer = LayerMask.NameToLayer (remoteLayerName);
	}

	// called when object is destroyed
	void OnDisable() {
		if (sceneCamera != null) {
			sceneCamera.gameObject.SetActive (true);
		}
	}
}
