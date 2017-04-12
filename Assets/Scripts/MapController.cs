using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapController : MonoBehaviour {

	private GameObject startMap;
	[SerializeField]
	private GameObject[] tiles;
	void Start () {
		startMap = GameObject.Find("StartMap");
		CreateMap ();
	}
	// Update is called once per frame
	void Update () {
		
	}
	private void CreateMap (){
		Vector3 start = startMap.transform.position;
		float tileSize = tiles[0].GetComponent<SpriteRenderer> ().sprite.bounds.size.x;
		int numTile;
		for (int y = 0; y < 14; y++) {
			for (int x = 0; x < 18; x++) {
				numTile = Random.Range(0,4);
				GameObject newTile = Instantiate(tiles[numTile]);
				newTile.transform.position = new Vector3 (start.x + tileSize * x, start.y - tileSize * y, 0);
			}
		}
	}
}
