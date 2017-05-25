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
		socket.On("player_connect", onPlayerConnected);
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

	}

	void onPlayerConnected(SocketIOEvent socketIOEvent){

	}

	void onOtherPlayerConnected(SocketIOEvent socketIOEvent){

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
	}

	// Update is called once per frame
	void Update () {
		
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
