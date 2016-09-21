using UnityEngine;
using System.Collections;

public abstract class ActivatableObject : MonoBehaviour {


    //True to prevent multiple activations in a single tap.
    //Set this to true the instant it is active, and set it to false when the object is no longer active, ready for the next tap.
    public bool triggered = false;
    public int numTimes = 0;

    //public GameObject raycastSource;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
    }

    public virtual void Activate() {
        if(!triggered) {
            DepressionFogScript.instance.makeFogBubble();
            DepressionFogScript.instance.makeFogBubble();
            triggered = true;
        }
    }

    public void Deactivate() {
    }

    void OnMouseDown()
    {
            Activate();
    }

    /// <summary>
    /// Create a text bubble with default 0,.5f,0 offset.
    /// </summary>
    /// <param name="message"></param>
    public GameObject makeBubble(string message) {
        return makeBubble(message, new Vector3(0f, 0.25f, 0f));
    }

    /// <summary>
    /// Create a text bubble that hovers over the object.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="offset"></param>
    public GameObject makeBubble(string message, Vector3 offset) {
        //Create the bubble above the object.
        GameObject bubble = Instantiate(OverallStatus.textBubblePrefab, transform.position, new Quaternion()) as GameObject;

        bubble.GetComponent<TextBubbleScript>().fullMessage = message;

        bubble.transform.SetParent(transform, true);
        bubble.transform.localPosition += offset;
        bubble.transform.parent = null;

        SpringJoint joint = this.gameObject.AddComponent<SpringJoint>();
        joint.connectedBody = bubble.GetComponent<Rigidbody>();
        joint.spring = 10f;
        joint.damper = 10f;
        //joint.maxDistance = 3;
        joint.anchor = offset;
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = Vector3.zero;

        return bubble;
    }
}
