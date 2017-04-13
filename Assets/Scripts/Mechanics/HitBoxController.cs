/* Cleaned */
using UnityEngine;

public class HitBoxController : MonoBehaviour {

  void Awake() {
    for(int i = 0; i < transform.childCount; i++) {
      transform.GetChild(i).gameObject.SetActive(false);
    }
  }

  public void TurnOn(int index) {
    for(int i = 0; i < transform.childCount; i++) {
      if(i != index) transform.GetChild(i).gameObject.SetActive(false);
      else transform.GetChild(i).gameObject.SetActive(true); 
    } 
  }

}
