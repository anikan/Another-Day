using UnityEngine;
using System.Collections;

public abstract class ActivatableObject : MonoBehaviour {

    //True while a tap is active and focused on it.
    public bool isActive;

    //True to prevent multiple activations in a single tap.
    //Set this to true the instant it is active, and set it to false when the object is no longer active, ready for the next tap.
    public bool triggered = false;
    public int numTimes = 0;

    public Object textBubblePrefab;

    public GameObject raycastSource;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void Activate() {
        isActive = true;
    }

    public void Deactivate() {
        isActive = false;
    }

    /// <summary>
    /// Create a text bubble with default 0,.5f,0 offset.
    /// </summary>
    /// <param name="message"></param>
    public void makeBubble(string message) {
        makeBubble(message, new Vector3(0, .1f, 0));
    }

    /// <summary>
    /// Create a text bubble that hovers over the object.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="offset"></param>
    public void makeBubble(string message, Vector3 offset) {
        //Create the bubble above the object.
        GameObject bubble = (GameObject)GameObject.Instantiate(textBubblePrefab, transform.position, new Quaternion());

        bubble.GetComponent<TextBubbleScript>().fullMessage = message;

        bubble.transform.SetParent(this.transform, true);
        bubble.transform.localPosition = offset;
        bubble.transform.SetParent(null);

        SpringJoint joint = this.gameObject.AddComponent<SpringJoint>();
        joint.connectedBody = bubble.GetComponent<Rigidbody>();
        joint.spring = 10f;
        joint.damper = 10f;
        //joint.maxDistance = 3;
        joint.anchor = offset;

        
    }
}
