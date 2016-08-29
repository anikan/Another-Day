using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhoneScript : ActivatableObject
{
    public List<MessageScript> messages;
    public GameObject ownMessagePrefab;
    public GameObject otherMessagePrefab;

    public Vector3 messageStartLocation;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Trigger the effect if not previously triggered this activation.
        if (isActive && !triggered)
        {
            triggered = true;
        }
    }

    void SendMessage(bool isOwn, string message)
    {
        if (isOwn)
        {
           // GameObject.Instantiate(ownMessagePrefab, this.transform, false, );
        }

        else
        {
           // GameObject.Instantiate(otherMessagePrefab, this.transform, false, )
        }
    }
}
