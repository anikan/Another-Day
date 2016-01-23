using UnityEngine;
using System.Collections;

public abstract class ActivatableObject : MonoBehaviour {

    //True while a tap is active and focused on it.
    public bool isActive;

    //True to prevent multiple activations in a single tap.
    //Set this to true the instant it is active, and set it to false when the object is no longer active, ready for the next tap.
    public bool triggered = false;
    public int numTimes = 0;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }
}
