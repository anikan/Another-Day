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

            makeBubble(message, new Vector3(0.0f, 0.0f, 0.0f));
            OverallStatus.doorChecked = true;

            base.Activate();
        }
    }
}
