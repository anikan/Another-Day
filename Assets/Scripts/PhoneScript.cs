using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Valve.VR;

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


    public GameObject thing;
    public string message = "I don't really want to talk to anyone.";


    public SteamVR_Controller.Device activeController;

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
            HighlightTutorial.turnOffTouchPadHL();
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
            activeController.TriggerHapticPulse();
            yield return null;
        }
        yield return null;
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

        //Start next song.
        MusicScript.instance.startNextSong();

        yield return new WaitForSeconds(1f);

        GameObject choice1 = makeBubble("Fine", new Vector3(-.5f, .1f, .75f));
        GameObject choice2 = makeBubble("Not feeling well", new Vector3(.5f, .1f, 0.75f));

        choice1.GetComponent<TextBubbleScript>().timeAfterDoneToDestroy = 999999;
        choice2.GetComponent<TextBubbleScript>().timeAfterDoneToDestroy = 999999;
        if(activeController != null)
            HighlightTutorial.turnOnTouchPadHL(thing);
       //TODO
       //Vive options.
       Vector2 p;
        /*   while(activeController == null ||
            !(activeController.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x < -.5f) &&
            !(activeController.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x > .5f)) {
            print("Waiting" + (activeController != null ? activeController.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad): Vector2.zero));
            yield return null;
        }*/
        while (activeController == null ||
            (!(activeController.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x < -.5f) &&
            !(activeController.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x > .5f)) || !activeController.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            yield return null;
        }
        p = activeController.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);

        //Choice has been selected.

        if (bubble != null)
        {
            bubble.GetComponent<TextBubbleScript>().destroy();
        }
        choice1.GetComponent<TextBubbleScript>().destroy();
        choice2.GetComponent<TextBubbleScript>().destroy();

        if (p.x < -.5f)
        {
            yield return FinePart();
        }

        else if (p.x > .5f)
        {
            yield return BadPart();
        }

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

        GameObject choice1 = makeBubble("Don't worry about it", new Vector3(-.5f, .1f, .75f));
        GameObject choice2 = makeBubble("Ok", new Vector3(.5f, .1f, 0.75f));

        choice1.GetComponent<TextBubbleScript>().timeAfterDoneToDestroy = 999999;
        choice2.GetComponent<TextBubbleScript>().timeAfterDoneToDestroy = 999999;

        while (activeController == null ||
    (!(activeController.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x < -.5f) &&
    !(activeController.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x > .5f)) || !activeController.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            yield return null;
        }


        Vector2 p = activeController.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);

        //Choice has been selected.

        if (bubble != null)
        {
            bubble.GetComponent<TextBubbleScript>().destroy();
        }
        choice1.GetComponent<TextBubbleScript>().destroy();
        choice2.GetComponent<TextBubbleScript>().destroy();

        if (p.x < -.5f)
        {
            yield return DontWorryPart();
        }

        else if (p.x > .5f)
        {
            yield return BadPart();
        }

    }

    IEnumerator BadPart()
    {
        GameObject bubble = makeBubble("Hmm.", new Vector3(0.0f, 1.0f, 0.0f));
        SendMessage(true, "Eh, not feeling that great, nothing much going on.");
        yield return new WaitForSeconds(1f);

        SendMessage(false, "Oh really, sick?");

        GameObject choice1 = makeBubble("Yeah", new Vector3(-.5f, .1f, .75f));
        GameObject choice2 = makeBubble("Kinda Empty", new Vector3(.5f, .1f, 0.75f));

        choice1.GetComponent<TextBubbleScript>().timeAfterDoneToDestroy = 999999;
        choice2.GetComponent<TextBubbleScript>().timeAfterDoneToDestroy = 999999;

        while (activeController == null ||
    (!(activeController.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x < -.5f) &&
    !(activeController.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x > .5f)) || !activeController.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            yield return null;
        }

        Vector2 p = activeController.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);

        //Choice has been selected.

        if (bubble != null)
        {
            bubble.GetComponent<TextBubbleScript>().destroy();
        }
        choice1.GetComponent<TextBubbleScript>().destroy();
        choice2.GetComponent<TextBubbleScript>().destroy();

        //Choice doesn't matter.

        SendMessage(false, "Still? Can't you snap out of it.");

        yield return DontWorryPart();
    }

    IEnumerator DontWorryPart()
    {
        SendMessage(true, "Don't worry about it.");
        yield return new WaitForSeconds(.5f);
        SendMessage(true, "Argh You can be so annoying!");
        yield return new WaitForSeconds(.5f);
        SendMessage(true, "Would you stop! I'm fine!.");
        GameObject bubble = makeBubble("Dang that was harsher than I intended.", new Vector3(0.0f, 1.0f, 0.0f));
        yield return new WaitForSeconds(1f);

        SendMessage(true, "I'm sorry");
        yield return new WaitForSeconds(1f);
        SendMessage(true, "I can't do anything right, can I");
        yield return new WaitForSeconds(1f);

        SendMessage(false, "No!");
        yield return new WaitForSeconds(.5f);

        SendMessage(false, "It's ok, I forgot");
        yield return new WaitForSeconds(.5f);

        SendMessage(false, "I need to be more careful about what I say.");
        yield return new WaitForSeconds(.5f);

        SendMessage(false, "If you need to talk, I'm here for you.");
        yield return new WaitForSeconds(.5f);

        bubble.GetComponent<TextBubbleScript>().destroy();

        bubble = makeBubble("Why are they bothering?", new Vector3(0.0f, 1.0f, 0.0f));

        SendMessage(true, "Thanks I guess");
        yield return new WaitForSeconds(.5f);

        SendMessage(true, "For now I think I'm gonna rest more");
        yield return new WaitForSeconds(.5f);

        SendMessage(true, "Bye");
        yield return new WaitForSeconds(2f);

        SendMessage(false, "Catch you later!");

        yield return new WaitForSeconds(.5f);

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

