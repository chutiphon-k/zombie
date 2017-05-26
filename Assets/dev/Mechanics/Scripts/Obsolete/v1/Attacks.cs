using UnityEngine;

namespace ObsoleteV1 {

  public class Attacks {
    
    CharacterStats callerStats; 
    DelayAction myAction;

    // Constructor
    public Attacks(CharacterStats stats) {
      callerStats = stats;
      myAction = new DelayAction();
    }

    public void NormalAttack(KeyCode key) {
      if(Input.GetKey(key)) {
        if(myAction.SignalWhenReady(1.0f / callerStats.ATKSPD)) {
          // Do some action
          Debug.Log("NormalAttack");
        }
      }
      else myAction.ResetTimer();
    }

    public void SkillAttack() {
      
    }

  }

}
