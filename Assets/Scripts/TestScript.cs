using UnityEngine;
using System.Collections;

public class TestScript : ActivatableObject {
    public Material mat1;
    public Material mat2;



    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Trigger the effect if not previously triggered this activation.
        if (isActive && !triggered)
        {
            triggered = true;
            numTimes++;
            this.GetComponent<Renderer>().material = mat2;
            Debug.Log("Triggered: " + numTimes);

        }

        //Turn off the triggering
        else if (!isActive && triggered)
        { 
            triggered = false;
            this.GetComponent<Renderer>().material = mat1;
            Debug.Log("Deactivated");
        }
    }
}
