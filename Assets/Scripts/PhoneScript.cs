using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Valve.VR;
using System;

public class PhoneScript : ActivatableObject
{
    //The content where scroll view objects go.
    public GameObject contentCanvas;

    public GameObject ownMessagePrefab;
    public GameObject otherMessagePrefab;

    private Vector3 ownMessageStartLocation = new Vector3(19, 0, 1.4f);
    private Vector3 otherMessageStartLocation = new Vector3(-19, 0, 1.4f);

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

    //True when conversation is finished. Used for new dialogue for things.
    bool convoFinished = false;

    public GameObject thing;


    public string message = "I don't really want to talk to anyone.";


    public SteamVR_Controller.Device activeController;

    // Use this for initialization
    void Start()
    {

    }

    public void StartConversation()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(Conversation());
    }

    // Update is called once per frame
    void Update()
    {
        //Trigger the effect if not previously triggered this activation.
        if (isActive && !triggered)
        {
           
            OverallStatus.phoneChecked = true;

            triggered = true;

            makeBubble(message);

        }

        //Trigger the effect if not previously triggered this activation.
        if (isActive && convoStarted && !triggeredAgain)
        {
            triggeredAgain = true;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            SendMessage(true, "H nackasd,l;a");
        }
    }

    /// <summary>
    /// Overloaded SendMessage that waits for time. To be used in a coroutine.
    /// </summary>
    /// <param name="isOwn"></param>
    /// <param name="message"></param>
    /// <param name="timeBeforeMessage"></param>
    /// <returns></returns>
    IEnumerator SendMessage(bool isOwn, string message, float timeBeforeMessage)
    {
        yield return new WaitForSeconds(timeBeforeMessage);

        SendMessage(isOwn, message);
    }

    void SendMessage(bool isOwn, string message)
    {
        GameObject messageObject;
        Vector3 messagePosition;

        if (isOwn)
        {
            messageObject = (GameObject)GameObject.Instantiate(ownMessagePrefab, contentCanvas.transform, true);
            messagePosition = ownMessageStartLocation;
        }

        else {
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

        contentCanvas.GetComponentInParent<ScrollRect>().velocity = new Vector2(0f, 100f);

        GetComponent<AudioSource>().Play();
        if (activeController != null)
            StartCoroutine(VibratePhone());
        //TODO
        //Vive vibrate.
    }

    IEnumerator VibratePhone()
    {
        for (float i = 0; i < .5f; i += Time.deltaTime)
        {
            if(activeController != null) {
                activeController.TriggerHapticPulse();
            }
            yield return null;
        }
        yield return null;
    }

    void HandleWaits()
    {

    }

    /// <summary>
    /// Overloaded method to not require bubbleObject.
    /// </summary>
    /// <param name="textLeft"></param>
    /// <param name="textRight"></param>
    /// <param name="leftChoice"></param>
    /// <param name="rightChoice"></param>
    /// <returns></returns>
    IEnumerator DoOption(string textLeft, string textRight, Func<IEnumerator> leftChoice, Func<IEnumerator> rightChoice)
    {
        yield return DoOption(textLeft, textRight, leftChoice, rightChoice, null);
    }


    /// <summary>
    /// Creates a choice and handles what to do with it.
    /// </summary>
    /// <param name="textLeft"></param>
    /// <param name="textRight"></param>
    /// <param name="leftChoice"> Left method to call</param>
    /// <param name="rightChoice"> Right method to call</param>
    /// <param name="bubbleObject">Bubble object from current dialogue, if set.</param>
    /// <returns></returns>
    IEnumerator DoOption(string textLeft, string textRight, Func<IEnumerator> leftChoice, Func<IEnumerator> rightChoice, GameObject bubbleObject)
    {
        GameObject choice1 = makeBubble(textLeft, new Vector3(-.5f, .1f, .75f));
        GameObject choice2 = makeBubble(textRight, new Vector3(.5f, .1f, 0.75f));

        choice1.GetComponent<TextBubbleScript>().timeAfterDoneToDestroy = 999999;
        choice2.GetComponent<TextBubbleScript>().timeAfterDoneToDestroy = 999999;

        if (activeController != null)
            HighlightTutorial.turnOnTouchPadHL(thing);

        Vector2 p = new Vector2(0,0);

        //Keep looping while no input was received.

        //2 main parts. 1 Checks vive. 2 checks mouse. 
        while ((activeController == null ||
            (!(activeController.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x < -.5f) &&
            !(activeController.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x > .5f)) || !activeController.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
            && (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1)))
        {
            yield return null;
        }

        HighlightTutorial.turnOffTouchPadHL();

        if (activeController != null)
        {
            p = activeController.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
        }
        //Choice has been selected.

        if (bubbleObject != null)
        {
            bubbleObject.GetComponent<TextBubbleScript>().destroy();
        }
        choice1.GetComponent<TextBubbleScript>().destroy();
        choice2.GetComponent<TextBubbleScript>().destroy();

        if (p.x < -.5f || Input.GetMouseButtonDown(0))
        {
            yield return leftChoice();
        }

        else if (p.x > .5f || Input.GetMouseButtonDown(1))
        {
            yield return rightChoice();
        }
    }

    IEnumerator Conversation()
    {
        convoStarted = true;
        SendMessage(false, "Hey, I haven't seen you in a while");
        //Create though bubble for sam.

        GameObject bubble = makeBubble("Probably Sam. \nMaybe worried about me?");
        yield return SendMessage(false, "What's up?", 1f);

        //Wait for player to pick up phone again.
        while (!triggeredAgain)
        {
            yield return null;
        }

        //Start next song when player gets phone.
        MusicScript.instance.startNextSong();

        yield return new WaitForSeconds(1f);

        //Go to fine or bad parts.
        yield return DoOption("Fine", "Not feeling well", FinePart, BadPart, bubble);

        MusicScript.instance.stopSong();
        yield return new WaitForSeconds(MusicScript.instance.timeToTransitionSong);
        MusicScript.instance.startNextSong();
    }

    IEnumerator FinePart()
    {
        GameObject bubble = makeBubble("I don't want Sam to worry.");
        SendMessage(true, "Oh ha, nothing much. Just... you know... the usual");
        yield return new WaitForSeconds(1f);

        SendMessage(false, "Then why haven't you been to classes?");

        //Go to dont worry or bad parts.
        yield return DoOption("Don't worry about it", "Ok fine", DontWorryPart, BadPart, bubble);
    }

    IEnumerator BadPart()
    {
        GameObject bubble = makeBubble("Hmm.", new Vector3(0.0f, .5f, 0.0f));
        SendMessage(true, "Eh, not feeling that great, nothing much going on.");
        yield return new WaitForSeconds(1f);

        SendMessage(false, "Oh really, sick?");

        //Go to dont sick or empty parts.
        yield return DoOption("Yeah", "Kinda Empty", SickPart, EmptyPart, bubble);

    }

    IEnumerator SickPart()
    {
        SendMessage(true, "Yeah, but it's not like being sad or anything.");
        yield return new WaitForSeconds(1f);

        SendMessage(true, "It's just feeling kinda empty");

        yield return DontWorryPart();
    }


    IEnumerator EmptyPart()
    {
        SendMessage(true, "No, just.");
        yield return new WaitForSeconds(1f);

        SendMessage(true, "Kinda empty.");

        yield return DontWorryPart();
    }
    
    IEnumerator DontWorryPart()
    {
        SendMessage(false, "Still? Can't you snap out of it.");

        SendMessage(true, "Don't worry about it.");
        yield return new WaitForSeconds(1.5f);
        SendMessage(true, "Argh You can be so annoying!");
        yield return new WaitForSeconds(1.5f);
        SendMessage(true, "Would you stop! I'm fine!.");
        GameObject bubble = makeBubble("Dang that was harsher than I intended.", new Vector3(0.0f, .5f, 0.0f));
        yield return new WaitForSeconds(4f);

        SendMessage(true, "I'm sorry");
        yield return new WaitForSeconds(1.5f);
        SendMessage(true, "I can't do anything right, can I");
        yield return new WaitForSeconds(2f);

        SendMessage(false, "No!");
        yield return new WaitForSeconds(1f);

        SendMessage(false, "It's ok, I forgot");
        yield return new WaitForSeconds(1f);

        SendMessage(false, "I need to be more careful about what I say.");
        yield return new WaitForSeconds(1f);

        SendMessage(false, "If you need to talk, I'm here for you.");

        bubble.GetComponent<TextBubbleScript>().destroy();

        bubble = makeBubble("Why are they bothering?", new Vector3(0.0f, .5f, 0.0f));

        yield return new WaitForSeconds(2f);

        SendMessage(true, "Thanks I guess");
        yield return new WaitForSeconds(1f);

        SendMessage(true, "For now I think I'm gonna rest more");
        yield return new WaitForSeconds(1f);

        SendMessage(true, "Bye");
        yield return new WaitForSeconds(2f);

        SendMessage(false, "Catch you later!");

        yield return new WaitForSeconds(1f);

        SendMessage(false, "And remember to hold on, at least for another day!");
    }


    IEnumerator ScrollWindow()
    {
        while (true)
        {
            //TODO
            //Vive scroll.
            //Vive code here. Essentially amount of touchpad scroll is velocity.
            contentCanvas.GetComponentInParent<ScrollRect>().velocity = new Vector2(0f, 10f); //Amount replaces 10f.

        }
    }
}

