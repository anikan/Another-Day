using UnityEngine;
using System.Collections;

public class PlaneScript : ActivatableObject {

    public GameObject directionalLight;
    public GameObject pointLight;

    public GameObject planeObject;

    public string message= "I don't think \nthat's a \ngood idea";
    public string startMessage = "It's \n so \n bright...";

    private GameObject initialBubble;
    // Use this for initialization
    void Start () {
        initialBubble = makeBubble(startMessage);
        initialBubble.GetComponent<TextBubbleScript>().timeAfterDoneToDestroy = 999999;
    }

    // Update is called once per frame
    void Update () {
        //Trigger the effect if not previously triggered this activation.
        if (isActive && !triggered)
        {
            if (initialBubble != null)
            {
                initialBubble.GetComponent<TextBubbleScript>().destroy();
            }

            triggered = true;
            numTimes++;

            directionalLight.SetActive(false);
            pointLight.SetActive(false);
            planeObject.SetActive(false);
            MusicScript.instance.startFirstSong();

            makeBubble(message);
        }

        //Turn off the triggering
        else if (!isActive && triggered)
        {
            triggered = false;
        }
    }
}
