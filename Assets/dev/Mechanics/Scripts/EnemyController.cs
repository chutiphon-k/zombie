using UnityEngine;

public class EnemyController : MonoBehaviour {

  // Dependencies
    Animator animator;

  void Update () {
		AnimatorSetVariables();
		// float step = speed * Time.deltaTime;
		// GameObject cantonment = GameObject.Find("Cantonment");
		// transform.position = Vector3.MoveTowards(transform.position, cantonment.transform.position, step);

		// currentPosition = transform.position;
		// if(currentPosition != oldPosition){
		// 	NetworkManager.instance.GetComponent<NetworkManager>().CommandEnemyMove(this.name, transform.position);
		// 	oldPosition = currentPosition;
		// }
	}

	void FixedUpdate() {
		HorizontalMove();
	}

  // Functions
		// TODO: หาวิธีการ move และการ attack เพื่อที่จะเอาไปใส่ใน animation ได้
    void AnimatorSetVariables() {

    }

    void HorizontalMove() {

    }

}