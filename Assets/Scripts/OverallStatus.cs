using UnityEngine;
using System.Collections;

public class OverallStatus : MonoBehaviour {

    public enum phoneCompleted
    {
        Nothing, Fine, Help, Ignore
    };

    public static phoneCompleted phoneStatus;

    public static bool doorChecked;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
