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

    public static bool doorChecked;


	// Use this for initialization
	void Start () {
        playerCamera = GameObject.Find("Camera (head)");
        textBubblePrefab = textBubblePrefabLocal;
  }
	
	// Update is called once per frame
	void Update () {
	
	}
}
