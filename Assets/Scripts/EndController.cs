using UnityEngine;
using System.Collections;

public class EndController : MonoBehaviour {
    public float openingTextDuration;
    public float secondMilestone;
    public float thirdMilestone;

    private float startTime;
    private float sadnessBlobsStart;
    private float happyBlobStart;
    private float lastBlobStart;

    private bool sadnessBlobs = false;
    private bool happyBlobs = false;
    private bool endBlobs = false;
    public GameObject bubbleController;
    public GameObject HappyBubbles;
    public GameObject LastBubbles;
	// Use this for initialization
	void Start () {

	  startTime = Time.time;
	  sadnessBlobsStart = startTime + openingTextDuration;
	  happyBlobStart = startTime + secondMilestone;
	  lastBlobStart = startTime = thirdMilestone;

	
	}
	
	// Update is called once per frame
	void Update () {

	  if (!sadnessBlobs && Time.time >= sadnessBlobsStart) {

	    sadnessBlobs = true;
	    bubbleController.SetActive(true);

	  }

	  if (!happyBlobs && Time.time >= happyBlobStart) {

	    happyBlobs = true;
	    HappyBubbles.SetActive(true);


	  }

	  if (!endBlobs && Time.time >= lastBlobStart) {

	    endBlobs = true;
	    LastBubbles.SetActive(true);


	  }
	}
}
