using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetworkManager : MonoBehaviour {
	private SocketIOComponent socket;
	public GameObject player;
	public GameObject enemyPrefab;
	private static Dictionary<string, Player> players = new Dictionary<string, Player> ();
	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();
		// SetupPlayer();
		StartCoroutine("SetupPlayer");
		socket.On("3", PlayerSpawn);
		// socket.On("4", )
	}

	void PlayerSpawn(SocketIOEvent evt) {
		JSONObject jsonData = evt.data.GetField("d");
		Debug.Log(jsonData[0]);
		EnemySpawn(jsonData[1]);
		// float xAxis = float.Parse(jsonData[1].ToString());
		// float yAxis = float.Parse(jsonData[2].ToString());
		// float zAxis = float.Parse(jsonData[3].ToString());
		// GameObject user = Instantiate(player,  new Vector3(xAxis, yAxis, zAxis), Quaternion.identity);
		// Debug.Log("Create Object");
	}

	void EnemySpawn(JSONObject enemies) {
		Debug.Log(enemies);
		foreach (var enemy in  enemies.list)
		{
			Debug.Log(enemy);
			// string enemyID = enemy[0];
			float xAxis = float.Parse(enemy[1].ToString());
			float yAxis = float.Parse(enemy[2].ToString());
			float zAxis = float.Parse(enemy[3].ToString());
			GameObject user = Instantiate(enemyPrefab, new Vector3(xAxis, yAxis, zAxis), Quaternion.identity);
			// RegisterPlayer(en)
		}
	}

	private IEnumerator SetupPlayer () {
		
		// 2: SETUP_PLAYER
		yield return new WaitForSeconds(1);
		Dictionary<string, string> data = new Dictionary<string, string>();
		data["username"] = "1234";
		Debug.Log("Create user");
		socket.Emit("2", new JSONObject(data));
	}

	public static void RegisterPlayer(string _netID, Player _player) {
		// string _playerID = PLAYER_ID_PREFIX + _netID;
		// players.Add (_playerID, _player);
		// _player.transform.name = _playerID;
	}
	
	// Update is called once per frame
	void Update () {
		// Debug.Log(socket.IsConnected);
	}
}
