using UnityEngine;
using System.Collections;

public class DiaryScript : ActivatableObject {

    public string messageOne = "My diary...";
    public string messageTwo = "I don't \n write \n anymore";
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }


    public override void Activate() {

        if(!triggered) {
            numTimes++;

            makeBubble(messageOne, new Vector3(-.25f, .25f, 0));
            makeBubble(messageTwo, new Vector3(.25f, .25f, 0));

            OverallStatus.diaryChecked = true;

            base.Activate();


        }
    }
}
