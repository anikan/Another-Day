using UnityEngine;
using System.Collections;

public class PhoneScript : ActivatableObject {

    private float locationThreshold = .01f;
    private Vector3 destination = new Vector3(.5f, -.1f, 1.5f);
    public float speed = 2f;
    private bool isMoving = false;

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
            transform.localRotation = Quaternion.Euler(0, 90, 90);
            transform.localPosition = new Vector3(.5f, -1f, 1.5f);
            isMoving = true;

        }

        //Move phone to view for convo.
        if (isMoving)
        {
            triggered = false;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, destination) < locationThreshold)
            {
                isMoving = false;
            }
        }
    }
}
