using UnityEngine;
using System.Collections;

public class TextBubbleScript : MonoBehaviour {

    public GameObject[] bubbleSprites;
    public GameObject raycastSource;

    public float cloudShiftTime = .1f;
    public float textSpeed = .1f;

    private float timeSinceLastCharacter = 0f;
    private float timeSinceLastCloud = 0f;

    private int activeCloudIndex = 0;

    //Message.
    public string fullMessage;
    public string messageSoFar;

    private int currentStringLength = 0;

    public TextMesh textBox;

	// Use this for initialization
	void Start () {
	    fullMessage = fullMessage.Replace("\\n", "\n");
        Debug.Log(fullMessage);
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(raycastSource.transform);

        timeSinceLastCharacter -= Time.deltaTime;
        timeSinceLastCloud -= Time.deltaTime;

        //Shift to the next cloud in the array.
        if ( timeSinceLastCloud < 0f)
        {
            //Hide old cloud.
            timeSinceLastCloud = cloudShiftTime;
            bubbleSprites[activeCloudIndex].SetActive(false);

            //Show new cloud.
            activeCloudIndex = (activeCloudIndex + 1) % (bubbleSprites.Length);
            bubbleSprites[activeCloudIndex].SetActive(true);
            bubbleSprites[activeCloudIndex].transform.Rotate(0, 0, 10f);
        }

        if (timeSinceLastCharacter < 0f && ((currentStringLength)  != fullMessage.Length))
        {
            timeSinceLastCharacter = textSpeed;
            messageSoFar = fullMessage.Substring(0, ++currentStringLength);
            textBox.text = messageSoFar;
        }
	}
}
