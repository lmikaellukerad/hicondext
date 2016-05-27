using UnityEngine;
using System.Collections;
using Leap;

namespace Leap.Unity{
  public class HandEnableDisable : HandTransitionBehavior {
    protected override void Awake() {
      base.Awake();
      gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    protected override void HandReset()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
  	}
  
  	// Use this for initialization
    protected override void HandFinish()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
  	}
  	
  }
}
