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
		socket.On("player_action", onPlayerAction);
		socket.On("player_health", onPlayerHealth);
		socket.On("enemies", onEnemies);
		socket.On("enemy_move", onEnemyMove);
		StartCoroutine(ConnectToServer());		
	}
	
	// Socket

	void onPlay(SocketIOEvent socketIOEvent){
		print("you joined");
		string data = socketIOEvent.data.ToString();
		UserJSON currentUserJSON = UserJSON.CreateFromJSON(data);
		print(currentUserJSON.name);
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
		Vector3 position = new Vector3(userJSON.position[0], userJSON.position[1], userJSON.position[2]);
		// Quaternion rotation = Quaternion.Euler(userJSON.rotation[0], userJSON.rotation[1], userJSON.rotation[2]);
		GameObject o = GameObject.Find(userJSON.name) as GameObject;
		if(o != null) return ;
		GameObject p = Instantiate(player, position, Quaternion.identity) as GameObject;
		PlayerController pc = p.GetComponent<PlayerController>();
		// Transform t = p.transform.Find("Healthbar Canvas");
		// Transform t1 = t.transform.Find("Player Name");
		// Text playerName = t1.GetComponent<Text>();
		// playerName.text = userJSON.name;
		pc.isLocalPlayer = false;
		p.name = userJSON.name;
		// Health h = p.GetComponent<Health>();
		// h.currentHealth = userJSON.health;
		// h.OnChangeHealth();
	}

	void onOtherPlayerDisconnected(SocketIOEvent socketIOEvent){
		print("player disconnected");
		string data = socketIOEvent.data.ToString();
		UserJSON userJSON = UserJSON.CreateFromJSON(data);
		Destroy(GameObject.Find(userJSON.name));
	}

	void onPlayerMove(SocketIOEvent socketIOEvent){
		string data = socketIOEvent.data.ToString();
		UserJSON userJSON = UserJSON.CreateFromJSON(data);
		Vector3 position = new Vector3(userJSON.position[0], userJSON.position[1], userJSON.position[2]);
		// if(userJSON.name == playerNameInput.text) return;
		GameObject p = GameObject.Find(userJSON.name) as GameObject;
		if(p != null) p.transform.position = position;
	}

	void onPlayerShoot(SocketIOEvent socketIOEvent){

	}

	void onPlayerBomb(SocketIOEvent socketIOEvent){

	}

	void onPlayerAction(SocketIOEvent socketIOEvent){
		string data = socketIOEvent.data.ToString();
		ActionJSON actionJSON = ActionJSON.CreateFromJSON(data);
		GameObject p = GameObject.Find(actionJSON.name);
		PlayerController pc = p.GetComponent<PlayerController>();
		switch(actionJSON.type){
			case "jump":
				pc.CmdJump();
			break;
			case "attack":
				pc.CmdAttack();
			break;
			case "bomb":
				pc.CmdBomb();
			break;
		}
	}

	void onPlayerHealth(SocketIOEvent socketIOEvent){

	}

	void onEnemies(SocketIOEvent socketIOEvent){
		print("enemy");
		EnemiesJSON enemiesJSON = EnemiesJSON.CreateFromJSON(socketIOEvent.data.ToString());
		EnemySpawner es = GetComponent<EnemySpawner>();
		es.SpawnEnemies(enemiesJSON);
	}

	void onEnemyMove(SocketIOEvent socketIOEvent){
		string data = socketIOEvent.data.ToString();
		UserJSON userJSON = UserJSON.CreateFromJSON(data);
		Vector3 position = new Vector3(userJSON.position[0], userJSON.position[1], userJSON.position[2]);
		// if(userJSON.name == playerNameInput.text) return;
		GameObject e = GameObject.Find(userJSON.name) as GameObject;
		if(e != null) e.transform.position = position;
	}

	// 

	IEnumerator ConnectToServer(){
		yield return new WaitForSeconds(0.5f);

		socket.Emit("player_connect");

		yield return new WaitForSeconds(1f);

		// string playerName = playerNameInput.text;
		string playerName = "eieiza" + UnityEngine.Random.Range(0f, 10f);
		List<SpawnPoint> playerSpawnPoints = GetComponent<PlayerSpawner>().playerSpawnPoints;
		List<SpawnPoint> enemySpawnPoints = GetComponent<EnemySpawner>().enemySpawnPoints;
		// PlayerJSON playerJSON = new PlayerJSON(playerName, playerSpawnPoints, enemySpawnPoints);
		PlayerJSON playerJSON = new PlayerJSON(playerName, playerSpawnPoints, enemySpawnPoints);
		string data = JsonUtility.ToJson(playerJSON);
		socket.Emit("play", new JSONObject(data));
		// canvas.gameObject.SetActive(false);
	}

	public void CommandMove(Vector3 vec3){
		string data = JsonUtility.ToJson(new PositionJSON(vec3));
		socket.Emit("player_move", new JSONObject(data));
	}

	public void CommandEnemyMove(string name, Vector3 vec3){
		UserJSON userJSON = new UserJSON();
		userJSON.position = new PositionJSON(vec3).position;
		userJSON.name = name;
		string data = JsonUtility.ToJson(userJSON);
		// print(data);
		socket.Emit("enemy_move", new JSONObject(data));
	}

	public void CommandAction(string type){
		// print("jump");
		ActionJSON actionJSON = new ActionJSON();
		actionJSON.type = type;
		string data = JsonUtility.ToJson(actionJSON);
		socket.Emit("player_action", new JSONObject(data));
	}

	// public void CommandHealthChange(GameObject playerFrom, GameObject playerTo, int healthChange, bool isEnemy){
	// 	print("health change cmd");
	// 	HealthChangeJSON healthChangeJSON = new HealthChangeJSON(playerTo.name, healthChange, playerFrom.name, isEnemy);
	// 	socket.Emit("health", new JSONObject(JsonUtility.ToJson(healthChangeJSON)));
	// }

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
		public List<PointJSON> enemySpawnPoints;

		public PlayerJSON(string _name, List<SpawnPoint> _playerSpawnPoints, List<SpawnPoint> _enemySpawnPoints){
			playerSpawnPoints = new List<PointJSON>();
			enemySpawnPoints = new List<PointJSON>();
			name = _name;
			foreach(SpawnPoint playerSpawnPoint in _playerSpawnPoints){
				PointJSON pointJSON = new PointJSON(playerSpawnPoint);
				playerSpawnPoints.Add(pointJSON);
			}
			foreach(SpawnPoint enemySpawnPoint in _enemySpawnPoints){
				PointJSON pointJSON = new PointJSON(enemySpawnPoint);
				enemySpawnPoints.Add(pointJSON);
			}
		}
	}

	[Serializable]
	public class EnemiesJSON {
		public List<UserJSON> enemies;

		public static EnemiesJSON CreateFromJSON(string data){
			return JsonUtility.FromJson<EnemiesJSON>(data);
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
		public float[] position;

		public PositionJSON(Vector3 _position){
			position = new float[] { _position.x, _position.y, _position.z };
		}
	}

	[Serializable]
	public class ActionJSON {
		public string name;
		public string type;
		public static ActionJSON CreateFromJSON(string data) {
			return JsonUtility.FromJson<ActionJSON>(data);
		}
	}	
}
