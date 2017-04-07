using System.Collections;
using System.Collections.Generic;

using UnityEngine;

// public class WeaponController : MonoBehaviour { 
// 	private bool isAttacking = false;
//   private PlayerController playerController;
//   private List<GameObject> targets = new List<GameObject>();

//   void Awake() {
//     playerController = GetComponentInParent<PlayerController>();
//   }

//   void Update() {
//     SetHitBox(playerController.GetDirection());
//   }

//   void OnTriggerStay2D(Collider2D other) {
//     if(other.tag == "Enemy" && !targets.Contains(other.gameObject))
//       targets.Add(other.gameObject); 
//   }

//   void OnTriggerExit2D(Collider2D other) {
//     targets.Remove(other.gameObject);
//   }

//   /* ***** PUBLIC FUNCTIONS ***** */
//     public IEnumerator AttackingManager(float animTime) {
//         isAttacking = true;
//         while(true) {
//           playerController._AttackWait();
//           yield return new WaitForSeconds(animTime);
//           playerController._Attacking();
//           Attack();

//           // to see result
//           yield return new WaitForSeconds(Mathf.Epsilon);
//         }
//       }

//       public bool GetAttackState() {
//         return isAttacking;
//       }

//       public void SetAttackState(bool value) {
//         isAttacking = value;
//       }
//   /* **************************** */

//   /* ***** PRIVATE FUNCTIONS ***** */
//     public void Attack() {
//       while(targets.Count > 0) {
//         GameObject toDelete = targets[0];
//         targets.RemoveAt(0);
//         Destroy(toDelete);
//       }
//     }

// 		void SetHitBox(string dir) {
//       if (dir == "up") {
//         transform.localRotation = Quaternion.Euler(0, 0, 90);
//         transform.localPosition = new Vector2(0, 1.0f + 0.25f + Mathf.Epsilon);
//       }
//       else if (dir == "down") {
//         transform.rotation = Quaternion.Euler(0, 0, 90);
//         transform.localPosition = new Vector2(0, -(1.0f + 0.25f + Mathf.Epsilon));
//       }
//       else if (dir == "left") {
//         transform.localPosition = new Vector2(-(1.0f + 0.25f + Mathf.Epsilon), 0);
//         transform.localRotation = Quaternion.identity;
//       }
//       else if (dir == "right") {
//         transform.localPosition = new Vector2(1.0f + 0.25f + Mathf.Epsilon, 0);
//         transform.localRotation = Quaternion.identity;
//       }     
//     }
//   /* ***************************** */

// }
