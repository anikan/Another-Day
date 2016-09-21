using UnityEngine;
using System.Collections;

public class GuitarScript : ActivatableObject {
    public string messageOne = "I used \nto play...";
    public string messageTwo = "Just not fun\n anymore.";
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKey(KeyCode.A)) {
            Activate();
        }

        if(Input.GetKey(KeyCode.S)) {
            Deactivate();
        }

    }

    public override void Activate() {

        if(!triggered) {
            numTimes++;

            makeBubble(messageOne, new Vector3(-.3f, .25f, 0));
            makeBubble(messageTwo, new Vector3(.3f, .25f, 0));
            OverallStatus.guitarChecked = true;

            base.Activate();


        }
    }


}
