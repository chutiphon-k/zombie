using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// public GameObject bulletPrefab;
	// public Transform bulletSpawn;
	public bool isLocalPlayer = false;
	public Material[] color;
	Vector3 oldPosition;
	Vector3 currentPosition;
	private Renderer rend;

	void Start () {
		oldPosition = transform.position;
		currentPosition = oldPosition;
		rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!isLocalPlayer) return;

		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
		transform.Translate(x, 0, 0);
		currentPosition = transform.position;

		if(currentPosition != oldPosition){
			NetworkManager.instance.GetComponent<NetworkManager>().CommandMove(transform.position);
			oldPosition = currentPosition;
		}
		NetworkManager n = NetworkManager.instance.GetComponent<NetworkManager>();
		if(Input.GetKeyDown(KeyCode.Space)){
			n.CommandAction("jump");
		} else if(Input.GetKeyDown(KeyCode.A)){
			n.CommandAction("attack");
		} else if(Input.GetKeyDown(KeyCode.S)){
			n.CommandAction("bomb");
		}
	}

	public void CmdJump(){
		rend.sharedMaterial = color[1];		
	}

	public void CmdAttack(){
		rend.sharedMaterial = color[2];		
	}

	public void CmdBomb(){
		rend.sharedMaterial = color[3];		
	}
}
