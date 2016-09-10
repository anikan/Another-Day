using UnityEngine;
using System.Collections;

public class BagelScript : ActivatableObject {
		public string message= "Not \nfeeling \nhungry";
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
			
			float distance = Vector3.Magnitude((transform.position - playerPosition) * .8f);

            makeBubble(message, new Vector3(0, .25f, 0));
		}
		
		//Turn off the triggering
		else if (!isActive && triggered)
		{
			triggered = false;
		}
	}
}
