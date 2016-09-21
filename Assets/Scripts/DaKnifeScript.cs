using UnityEngine;
using System.Collections;

public class DaKnifeScript : ActivatableObject {
    public string message = "...";
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public override void Activate() {

        if(!triggered) {
            OverallStatus.knifeChecked = true;

            numTimes++;

            makeBubble(message);

            base.Activate();
        }
    }
}
