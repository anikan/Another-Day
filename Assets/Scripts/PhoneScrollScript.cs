using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PhoneScrollScript : MonoBehaviour {

    ScrollRect rect;

	// Use this for initialization
	void Start () {
        rect = GetComponent<ScrollRect>();
	}
	
	// Update is called once per frame
	void Update () {
	
        if (Input.GetKeyDown(KeyCode.A))
        {
        //    rect.

           // rect.CalculateLayoutInputVertical();
        }

        else if (Input.GetKeyDown(KeyCode.Z))
        {
            rect.velocity = new Vector2(0f, 1000f);

        }
    }
}
