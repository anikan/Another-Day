﻿using UnityEngine;
using System.Collections;

public class DoorScript : ActivatableObject {

    public GameObject raycastSource;
    public string message= "I don't think \nthat's a \ngood idea";
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

            Vector3 playerPosition = raycastSource.transform.position;

            float distance = Vector3.Magnitude((transform.position - playerPosition) * .7f);
                
            //Create the bubble halfway between the object and the player.
            GameObject bubble = (GameObject)GameObject.Instantiate(textBubblePrefab, playerPosition + distance * raycastSource.transform.forward, new Quaternion());

            bubble.GetComponent<TextBubbleScript>().fullMessage = message;
        }

        //Turn off the triggering
        else if (!isActive && triggered)
        {
            triggered = false;
        }
    }
}
