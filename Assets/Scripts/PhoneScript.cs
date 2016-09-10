using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PhoneScript : ActivatableObject
{
    //The content where scroll view objects go.
    public GameObject contentCanvas;

    public GameObject ownMessagePrefab;
    public GameObject otherMessagePrefab;

    public Vector3 messageStartLocation;

    float bubbleOffset = 20;

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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SendMessage(true, "Oh Sad soul. Bio is slightly better imo\nw\nefwe");
        }
    }

    void SendMessage(bool isOwn, string message)
    {
        if (isOwn)
        {
            GameObject messageObject = (GameObject) GameObject.Instantiate(ownMessagePrefab, this.transform, false);
            messageObject.transform.parent = phoneCanvas.transform;


            //First get the size of the text.
            Text text = messageObject.GetComponentInChildren<Text>();
            TextGenerator textGen = text.cachedTextGenerator;

            float textHeight = textGen.GetPreferredHeight(message, text.GetGenerationSettings(text.rectTransform.rect.size));

            //Then update the image and the text itself.
            //Image
            RectTransform messageRect = messageObject.GetComponent<RectTransform>();

            Vector2 newRectSize = messageRect.sizeDelta + new Vector2(0, textHeight + bubbleOffset);

            messageRect.sizeDelta = new Vector2(newRectSize.x, newRectSize.y);

            //Text
            newRectSize = text.rectTransform.sizeDelta + new Vector2(0, textHeight + bubbleOffset);

            text.rectTransform.sizeDelta = new Vector2(newRectSize.x, newRectSize.y);

            text.text = message;
        }

        else
        {
           // GameObject.Instantiate(otherMessagePrefab, this.transform, false, )

            //Vibrate
            //Scroll down.
            //Play sound.
        }
    }
}
