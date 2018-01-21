using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

	[SerializeField]
	Behaviour[] componentsToDisable;

	[SerializeField]
	string remoteLayerName = "RemotePlayer";

	[SerializeField]
	string dontDrawLayerName = "DontDraw";

	[SerializeField]
	GameObject playerGraphics;

	[SerializeField]
	GameObject playerUIPrefab;

	private GameObject playerUIInstance;

	private Camera sceneCamera;

	void Start() {
		Debug.Log(isLocalPlayer);
		// if (!isLocalPlayer) {
		// 	DisableComponents ();
		// 	AssignRemotePlayer ();
		// }

		// else {
			sceneCamera = Camera.main;
			if (sceneCamera != null) {
				sceneCamera.gameObject.SetActive (false);
			}

			// Disable player graphics for LocalPlayer
			SetLayerRecursively(playerGraphics, LayerMask.NameToLayer(dontDrawLayerName));

			// Create PlayerUI
			playerUIInstance = Instantiate(playerUIPrefab);
			playerUIInstance.name = playerUIPrefab.name;
		// }

		GetComponent<Player> ().Setup ();

	}

	void SetLayerRecursively(GameObject obj, int newLayer) {
		obj.layer = newLayer;

		foreach (Transform child in obj.transform) {
			SetLayerRecursively (child.gameObject, newLayer);
		}
	}

	public override void OnStartClient() {
		base.OnStartClient ();
		string _netID = GetComponent<NetworkIdentity> ().netId.ToString();
		Player _player = GetComponent<Player> ();
		GameManager.RegisterPlayer (_netID, _player);
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

		Destroy (playerUIInstance);

		if (sceneCamera != null) {
			sceneCamera.gameObject.SetActive (true);
		}

		GameManager.DeRegisterPlayer (transform.name);
	}
}
