﻿using UnityEngine;
using System.Collections;

public class DiaryScript : ActivatableObject {
	
	public GameObject raycastSource;
	public string messageOne= "My diary...";
	public string messageTwo= "I don't \n write \n anymore";
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
			
			float distanceOne = Vector3.Magnitude((transform.position - playerPosition) * .8f);
			
			//Create the bubble halfway between the object and the player.
			GameObject bubbleOne = (GameObject)GameObject.Instantiate(textBubblePrefab, playerPosition + distanceOne * raycastSource.transform.forward + new Vector3(-2,1,-1), new Quaternion());
			GameObject bubbleTwo = (GameObject)GameObject.Instantiate(textBubblePrefab, playerPosition + distanceOne * raycastSource.transform.forward + new Vector3(2,1,1), new Quaternion());

			bubbleOne.GetComponent<TextBubbleScript>().fullMessage = messageOne;
			bubbleTwo.GetComponent<TextBubbleScript>().fullMessage = messageTwo;
		}
		
		//Turn off the triggering
		else if (!isActive && triggered)
		{
			triggered = false;
		}
	}
}
