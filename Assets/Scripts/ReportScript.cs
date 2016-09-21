//using UnityEngine;
//using System.Collections;

//public class ReportScript : ActivatableObject {

//    public string message1= "How did \nthis \nhappen...";
//	public string message2= "Eh, it \ndoesn't \nmatter";
//	// Use this for initialization
//	void Start () {
		
//	}
	
//	// Update is called once per frame
//	void Update () {
//		//Trigger the effect if not previously triggered this activation.
//		if (isActive && !triggered)
//		{
//			triggered = true;
//			numTimes++;

//            makeBubble(message1, new Vector3(-.25f, .25f, 0));
//            makeBubble(message2, new Vector3(.25f, .25f, 0));
//		}
		
//		//Turn off the triggering
//		else if (!isActive && triggered)
//		{
//			triggered = false;
//		}
//	}
//}
