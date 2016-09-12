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

    private Vector3 ownMessageStartLocation = new Vector3(19, 0, 1.4f);
    private Vector3 otherMessageStartLocation = new Vector3(-19,0, 1.4f);

    const float FIRST_MESSAGE_HEIGHT = -2.5f;
    float nextMessageHeight = FIRST_MESSAGE_HEIGHT;

    //Distance between text and the bubble
    float textOffset = 5;

    //Distance between bubbles.
    float bubbleOffset = 5;

    //Time to wait before complaining about not responding.
    float waitTime = 10;

    //How many times has Sam waited over the waitTime?
    int amountTooMuchWait = 0;

    bool triggeredAgain = false;
    bool convoStarted = false;

    // Use this for initialization
    void Start()
    {
        StartConversation();
    }

    public void StartConversation()
    {
        StartCoroutine(Conversation());
    }

    // Update is called once per frame
    void Update()
    {
        //Trigger the effect if not previously triggered this activation.
        if (isActive && !triggered)
        {
            triggered = true;
        }

        //Trigger the effect if not previously triggered this activation.
        if (isActive && convoStarted && !triggeredAgain)
        {
            triggeredAgain = true;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SendMessage(true, "Oh Sad soul. Bio is slightly better imo\nw\nefwe");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            SendMessage(false, "Pls sceptile is cool\nw\nefwe");
        }
    }

    void SendMessage(bool isOwn, string message)
    {
        GameObject messageObject;
        Vector3 messagePosition;

        if (isOwn)
        {
            messageObject = (GameObject) GameObject.Instantiate(ownMessagePrefab, contentCanvas.transform, true);
            messagePosition = ownMessageStartLocation;
        }

        else
        {
            messageObject = (GameObject)GameObject.Instantiate(otherMessagePrefab, contentCanvas.transform, true);
            messagePosition = otherMessageStartLocation;

            // GameObject.Instantiate(otherMessagePrefab, this.transform, false, )

            //Vibrate
            //Scroll down.
            //Play sound.
        }

        //First get the size of the text.
        Text text = messageObject.GetComponentInChildren<Text>();
        text.text = message;

        float textHeight = text.preferredHeight;

        //Then update the image and the text itself.
        //Image
        RectTransform messageRect = messageObject.GetComponent<RectTransform>();

        Vector2 newRectSize = messageRect.sizeDelta;
        newRectSize.y = textHeight + textOffset;

        messageRect.sizeDelta = new Vector2(newRectSize.x, newRectSize.y);

        //Text
        newRectSize = text.rectTransform.sizeDelta;
        newRectSize.y = textHeight;

        text.rectTransform.sizeDelta = new Vector2(newRectSize.x, newRectSize.y);

        //Set position of message.
        messagePosition += new Vector3(0, nextMessageHeight, 0);
        messageObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        messageObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

        //Need to reset z to 0.
        messageObject.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        messageRect.anchoredPosition = messagePosition;

        nextMessageHeight -= (textHeight + textOffset + bubbleOffset);

        //Increase size of content.
        RectTransform contentRect = contentCanvas.GetComponent<RectTransform>();

        Vector2 contentSize = contentRect.sizeDelta;
        contentSize.y += textHeight + textOffset + bubbleOffset;
        contentRect.sizeDelta = contentSize;

        contentCanvas.GetComponentInParent<ScrollRect>().velocity = new Vector2(0f, 10f);
    }

    void HandleWaits()
    {

    }

    IEnumerator Conversation()
    {
        convoStarted = true;
        SendMessage(false, "Hey, I haven't seen you in a while");
        //Create though bubble for sam.

        yield return new WaitForSeconds(1f);
        GameObject bubble = makeBubble("Probably Sam. \nMaybe worried about me?");
        SendMessage(false, "What's up?");

        //Wait for player to pick up phone again.
        while (!triggeredAgain)
        {
            yield return null;
        }

        MusicScript.instance.startNextSong();
        
        yield return new WaitForSeconds(1f);

        GameObject choice1 = makeBubble("Fine", new Vector3(-.5f, .1f, .75f));
        GameObject choice2 = makeBubble("Not feeling well", new Vector3(.5f, .1f, 0.75f));

        while (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
        {
            yield return null;
        }
        
        //Choice has been selected.
        bubble.GetComponent<TextBubbleScript>().destroy();
        choice1.GetComponent<TextBubbleScript>().destroy();
        choice2.GetComponent<TextBubbleScript>().destroy();

        if (Input.GetMouseButtonDown(0))
        {
            yield return FinePart();
        }

        else if (Input.GetMouseButtonDown(1))
        {
            yield return BadPart();
        }

        MusicScript.instance.stopSong();
        yield return new WaitForSeconds(MusicScript.instance.timeToTransitionSong);
        MusicScript.instance.startNextSong();
    }

    IEnumerator FinePart()
    {
        makeBubble("I don't want Sam to worry.");
        SendMessage(true, "Oh ha, nothing much. Just... you know... the usual");
        yield return new WaitForSeconds(1f);
    }

    IEnumerator BadPart()
    {
        makeBubble("Hmm.", new Vector3(0.0f, 1.0f, 0.0f));
        SendMessage(true, "Eh, not feeling that great, nothing much.");
        yield return new WaitForSeconds(1f);
        yield return null;
    }


}
