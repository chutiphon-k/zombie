using System;
using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

	public static NetworkManager instance;
	public SocketIOComponent socket;
	public GameObject player;

	void Awake(){
		if(instance == null)
			instance = this;
		else if(instance != this)
			Destroy(gameObject);
			DontDestroyOnLoad(gameObject);
	}

	void Start () {	
		socket.On("play", onPlay);
		socket.On("other_player_connected", onOtherPlayerConnected);
		socket.On("other_player_disconnected", onOtherPlayerDisconnected);
		socket.On("player_move", onPlayerMove);
		socket.On("player_shoot", onPlayerShoot);
		socket.On("player_bomb", onPlayerBomb);
		socket.On("player_jump", onPlayerJump);
		socket.On("player_health", onPlayerHealth);
		socket.On("enmies", onEnemies);
		StartCoroutine(ConnectToServer());		
	}
	
	// Socket

	void onPlay(SocketIOEvent socketIOEvent){
		print("you joined");
		string data = socketIOEvent.data.ToString();
		UserJSON currentUserJSON = UserJSON.CreateFromJSON(data);
		Vector3 position = new Vector3(currentUserJSON.position[0], currentUserJSON.position[1], currentUserJSON.position[2]);
		// Quaternion rotation = Quaternion.Euler(currentUserJSON.rotation[0], currentUserJSON.rotation[1], currentUserJSON.rotation[2]);
		GameObject p = Instantiate(player, position, Quaternion.identity) as GameObject;
		PlayerController pc = p.GetComponent<PlayerController>();
		// Transform t = p.transform.Find("Healthbar Canvas");
		// Transform t1 = t.transform.Find("Player Name");
		// Text playerName = t1.GetComponent<Text>();
		// playerName.text = currentUserJSON.name;
		pc.isLocalPlayer = true;
		p.name = currentUserJSON.name;
	}

	void onOtherPlayerConnected(SocketIOEvent socketIOEvent){
		print("someone joined");
		string data = socketIOEvent.data.ToString();
		UserJSON userJSON = UserJSON.CreateFromJSON(data);
		print(userJSON.position.ToString());
	}

	void onOtherPlayerDisconnected(SocketIOEvent socketIOEvent){

	}

	void onPlayerMove(SocketIOEvent socketIOEvent){

	}

	void onPlayerShoot(SocketIOEvent socketIOEvent){

	}

	void onPlayerBomb(SocketIOEvent socketIOEvent){

	}

	void onPlayerJump(SocketIOEvent socketIOEvent){

	}

	void onPlayerHealth(SocketIOEvent socketIOEvent){

	}

	void onEnemies(SocketIOEvent socketIOEvent){

	}

	// 

	IEnumerator ConnectToServer(){
		yield return new WaitForSeconds(0.5f);

		socket.Emit("player_connect");

		yield return new WaitForSeconds(1f);

		// string playerName = playerNameInput.text;
		string playerName = "eieiza555";
		List<SpawnPoint> playerSpawnPoints = GetComponent<PlayerSpawner>().playerSpawnPoints;
		// List<SpawnPoint> enemySpawnPoints = GetComponent<EnemySpawner>().enemySpawnPoints;
		// PlayerJSON playerJSON = new PlayerJSON(playerName, playerSpawnPoints, enemySpawnPoints);
		PlayerJSON playerJSON = new PlayerJSON(playerName, playerSpawnPoints);
		string data = JsonUtility.ToJson(playerJSON);
		socket.Emit("play", new JSONObject(data));
		// canvas.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		
	}

	[Serializable]
	public class PointJSON {
		public float[] position;

		public PointJSON(SpawnPoint spawnPoint){
			position = new float[] {
				spawnPoint.transform.position.x,
				spawnPoint.transform.position.y,
				spawnPoint.transform.position.z
			};
		}
	}

	[Serializable]
	public class PlayerJSON {
		public string name;
		public List<PointJSON> playerSpawnPoints;
		// public List<PointJSON> enemySpawnPoints;

		// public PlayerJSON(string _name, List<SpawnPoint> _playerSpawnPoints, List<SpawnPoint> _enemySpawnPoints){
		public PlayerJSON(string _name, List<SpawnPoint> _playerSpawnPoints){
			playerSpawnPoints = new List<PointJSON>();
			// enemySpawnPoints = new List<PointJSON>();
			name = _name;
			foreach(SpawnPoint playerSpawnPoint in _playerSpawnPoints){
				PointJSON pointJSON = new PointJSON(playerSpawnPoint);
				playerSpawnPoints.Add(pointJSON);
			}
			// foreach(SpawnPoint enemySpawnPoint in _enemySpawnPoints){
			// 	PointJSON pointJSON = new PointJSON(enemySpawnPoint);
			// 	enemySpawnPoints.Add(pointJSON);
			// }
		}
	}

	[Serializable]
	public class UserJSON {
		public string name;
		public float[] position;
		public int health;

		public static UserJSON CreateFromJSON(string data){
			return JsonUtility.FromJson<UserJSON>(data);
		}
	}

	[Serializable]
	public class PositionJSON {
		public string name;
		public float[] position;
		public int health;

		public static UserJSON CreateFromJSON(string data){
			return JsonUtility.FromJson<UserJSON>(data);
		}
	}

	[Serializable]
	public class ShootJSON {
		public string name;
		public static ShootJSON CreateFromJSON(string data) {
			return JsonUtility.FromJson<ShootJSON>(data);
		}
	}	
}
