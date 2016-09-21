using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DepressionFogScript : MonoBehaviour
{
    public static DepressionFogScript instance;
    private List<string> quotes;
    private float sphereRadius = 2;
    private List<GameObject> bubbleList = new List<GameObject>();

    void Awake()
    {
        if (instance == null) {

            instance = this;

        }
    }

    // Use this for initialization
    void Start()
    {

        quotes = new List<string>();
        quotes.Add("Now I'm just wallowing in self pity");
        quotes.Add("I can't do anything right");
        quotes.Add("Why bother, I'm doomed anyways");
        quotes.Add("I can't trust anyone");
        quotes.Add("I'm a terrible child");
        quotes.Add("I'm a terrible friend");
        quotes.Add("I'm a terrible sibling");
        quotes.Add("Why can't I stop thinking like this");
        quotes.Add("I need a break from everything");
        quotes.Add("I don't deserve what I have");
        quotes.Add("My friends would be better off without me");
        quotes.Add("My friends probably merely tolerate me");
        quotes.Add("I don't want to go back to pretending to be happy and normal");
        quotes.Add("I'm just a problem to those close to me");
        quotes.Add("I will die having done nothing useful in the end");
        quotes.Add("I shouldn't have gotten so close to my friends");
        quotes.Add("I'm so self-centered");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            makeFogBubble();
        }
    }

    /// <summary>
    /// A version of makeBubble that sets the anchor to the head, causing the bubble to idly float around the head. Also doesn't disappear
    /// </summary>
    /// <param name="objectToConnectTo"></param>
    /// <param name="position"></param>
    /// <returns>A bubble</returns>
    public GameObject makeFogBubble()
    {
        //Create the bubble above the object.
        GameObject bubble = Instantiate(OverallStatus.textBubblePrefab, this.transform.position, new Quaternion()) as GameObject;

        if(bubble != null && quotes != null) {

            bubble.GetComponent<TextBubbleScript>().fullMessage = quotes[Random.Range(0, quotes.Count)];

        }

        bubble.GetComponent<Rigidbody>().drag = 0f;
        bubble.transform.SetParent(transform, true);
        bubble.transform.localPosition = new Vector3(0,0, sphereRadius);

        bubble.GetComponent<Rigidbody>().AddForce(transform.InverseTransformDirection(new Vector3(Random.Range(-.05f, .05f), Random.Range(-.05f, .05f), 0.0f)), ForceMode.Impulse);
        
        bubble.transform.SetParent(null);

        SpringJoint joint = gameObject.AddComponent<SpringJoint>();
        joint.connectedBody = bubble.GetComponent<Rigidbody>();
        joint.spring = .5f;
        joint.damper = 10f;
        //joint.maxDistance = 3;
        joint.anchor = new Vector3(0.0f, 0.0f, 0.0f);

        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = new Vector3(0, 0, 0);

        //These bubbles don't disappear until specified points.
        bubble.GetComponent<TextBubbleScript>().timeAfterDoneToDestroy = 999999999;

        bubbleList.Add(bubble);

        return bubble;
    }
}
