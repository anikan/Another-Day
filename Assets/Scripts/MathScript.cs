using UnityEngine;
using System.Collections;

public class MathScript : ActivatableObject {
	public string message1= "What a\n drag";
	public string message2= "I was good\n at math...";
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

            makeBubble(message1, new Vector3(-.25f, .25f, 0));
            makeBubble(message2, new Vector3(.25f, .25f, 0));	
		}
		
		//Turn off the triggering
		else if (!isActive && triggered)
		{
			triggered = false;
		}
	}
}
