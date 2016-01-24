using UnityEngine;
using System.Collections;

public class PhoneScript : ActivatableObject {

    public GameObject raycastSource;

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
            transform.parent = raycastSource.transform;

        }

        //Turn off the triggering
        else if (!isActive && triggered)
        {
            triggered = false;
        }
    }
}
