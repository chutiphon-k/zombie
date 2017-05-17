using UnityEngine;

namespace ObsoleteV1 {

  public class DelayAction {

    float startTime = 0.0f;
    bool actionLock = false;

    public void ResetTimer() {
      startTime = Time.time;
    }

    public bool SignalWhenReady(float waitTime) {
      if (!actionLock) {
        actionLock = true;
        startTime = Time.fixedTime;
        return false;
      }
      else {
        if (Time.fixedTime >= startTime + waitTime) {
          actionLock = false;
          return true;
        }
        return false;
      }
    } 

  }

}