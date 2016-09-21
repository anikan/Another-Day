using UnityEngine;
using System.Collections;

public class HistoryScript : ActivatableObject {

    public string message= "I don't \n feel up to \n work...";
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

            OverallStatus.textbookChecked = true;

            base.Activate();

        }

    }
}
