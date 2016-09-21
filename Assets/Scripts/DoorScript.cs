using UnityEngine;
using System.Collections;

public class DoorScript : ActivatableObject {

    public string message= "I don't think \nthat's a \ngood idea";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    public override void Activate() {
        if(!triggered) {
            numTimes++;

            makeBubble(message);
            OverallStatus.doorChecked = true;

            base.Activate();
        }
    }
}
