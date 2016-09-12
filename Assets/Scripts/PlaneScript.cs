using UnityEngine;
using System.Collections;

public class PlaneScript : ActivatableObject {

    public GameObject directionalLight;
    public GameObject pointLight;

    public GameObject planeObject;

    public string message= "I don't think \nthat's a \ngood idea";
    public string startMessage = "It's \n so \n bright...";
    // Use this for initialization
    void Start () {
        makeBubble(startMessage);

    }

    // Update is called once per frame
    void Update () {
        //Trigger the effect if not previously triggered this activation.
        if (isActive && !triggered)
        {
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
