using UnityEngine;
using System.Collections;

public class TextBubbleScript : MonoBehaviour {

    public GameObject[] bubbleSprites;
    private GameObject raycastSource;

    public float cloudShiftTime = .1f;
    public float textSpeed = .1f;

    private float timeSinceLastCharacter = 0f;
    private float timeSinceLastCloud = 0f;

    private int activeCloudIndex = 0;

    public float timeAfterDoneToDestroy = 2f;

    //Message variables
    public string fullMessage;
    public string messageSoFar;

    private int currentStringLength = 0;

    public TextMesh textBox;

    public Animator animator;

    // Use this for initialization
    void Start () {
	    fullMessage = fullMessage.Replace("\\n", "\n");
        raycastSource = GameObject.Find("Raycast Source");
        animator = GetComponent<Animator>();
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
            //bubbleSprites[activeCloudIndex].transform.Rotate(0, 0, 10f);
        }

        //Add new character if time has elapsed and there are characters left.
        if (timeSinceLastCharacter < 0f && ((currentStringLength)  != fullMessage.Length))
        {
            timeSinceLastCharacter = textSpeed;
            messageSoFar = fullMessage.Substring(0, ++currentStringLength);
            textBox.text = messageSoFar;

            //The last one. Destroy in a bit.
            if ((currentStringLength) == fullMessage.Length)
            {
                animator.SetBool("Ending", true);
                Destroy(this.gameObject, timeAfterDoneToDestroy);
            }
        }
	}
}
