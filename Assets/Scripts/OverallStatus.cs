using UnityEngine;
using System.Collections;

public class OverallStatus : MonoBehaviour {

    public enum phoneCompleted
    {
        Nothing, Fine, Help, Ignore
    };

    public static phoneCompleted phoneStatus;
    public static GameObject playerCamera;
    public static GameObject textBubblePrefab;
    public GameObject textBubblePrefabLocal;
    public PhoneScript phone;
    public GameObject guiCam;

    public static bool doorChecked;
    public static bool knifeChecked;
    public static bool windowChecked;
    public static bool guitarChecked;
    public static bool diaryChecked;
    public static bool textbookChecked;
    public static bool phoneChecked;


    
    // Use this for initialization
    void Awake () {
        playerCamera = GameObject.Find("Camera (eye)");
        textBubblePrefab = textBubblePrefabLocal;
  }
	
	// Update is called once per frame
	void Update () {
        guiCam.transform.position = playerCamera.transform.position;
        
      //  print(guiCam.transform.position + " " + playerCamera.transform.position);
        guiCam.transform.rotation = playerCamera.transform.rotation;

        if(doorChecked && knifeChecked && windowChecked && guitarChecked && diaryChecked && textbookChecked && phoneChecked)
        {
            MusicScript.instance.stopSong();
            phone.StartConversation();
        }
    }
}
