using UnityEngine;
using System.Collections;

public class DoorScript : ActivatableObject {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //Trigger the effect if not previously triggered this activation.
        if (isActive && !triggered)
        {
            triggered = true;
            numTimes++;

            Vector3 result = new Vector3 (transform.position.x * .8f, transform.position.y, transform.position.z * .8f);

            //Create the bubble halfway between the object and the player.
            GameObject bubble = (GameObject)GameObject.Instantiate(textBubblePrefab, result, new Quaternion());

            bubble.GetComponent<TextBubbleScript>().fullMessage = "I don't think \nthat's a \ngood idea";
        }

        //Turn off the triggering
        else if (!isActive && triggered)
        {
            triggered = false;
        }
    }
}
