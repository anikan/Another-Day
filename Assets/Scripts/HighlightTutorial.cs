using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HighlightTutorial:MonoBehaviour {

    public static HighlightTutorial instance;
    public  List<GameObject> grab = new List<GameObject>();
    public  List<GameObject> touchpad = new List<GameObject>();

    public static Color32 blue = new Color32(0x22, 0x22, 0x6C, 0xFF);
    public static Color32 red = new Color32(0x83, 0x35, 0x35, 0xFF);

    void Awake() {
        instance = this;
        //print("awake");
    
        //print(touchpad.Count);
      
    }
    
    public void turnOnGrabHL(GameObject controller) {
        foreach(GameObject item in grab) {
            item.SetActive(true);
            StartCoroutine(LeftLerp(item));
        }
    }
    public void turnOffGrabHL() {
        foreach(GameObject item in grab) {
            item.SetActive(false);
        }
    }

    public void turnOnTouchPadHL(GameObject controller) {
        //print("here");
        foreach(GameObject item in touchpad) {
            item.SetActive(true);
            Renderer[] mats = item.GetComponentsInChildren<Renderer>(); 
            switch(item.tag) {
                case "leftGood":
                    foreach(Renderer items in mats) {
                        StartCoroutine(LeftLerp(items.gameObject));
                    }
                    
                    break;
                case "rightTag":
                    foreach(Renderer items in mats) {
                        StartCoroutine(RightLerp(items.gameObject));
                    }
                    break;
                default:
                    print("failed");
                    break;
            }
        }
    }



    public void turnOffTouchPadHL() {
        print("turning off touch pad");
        foreach(GameObject item in touchpad) {
            item.SetActive(false);
        }
    }
    float time = 1f;
    IEnumerator LeftLerp(GameObject item) {
        while(true) {
            
            for(float i = 0; i < time; i += Time.deltaTime) {
                item.GetComponentInChildren<Renderer>().material.SetColor("_EmissionColor",  Color.Lerp(Color.black, blue, i / time));
                //print("LERPING");
                yield return null;
            }
            for(float i = 0; i < time; i += Time.deltaTime) {
                //print("LERPING");
                item.GetComponentInChildren<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(blue, Color.black, i / time));
                yield return null;
            }
           
        } 
    }
    IEnumerator RightLerp(GameObject item) {
        while(true) {
            for(float i = 0; i < time; i += Time.deltaTime) {
                item.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.black, red, i / time));
                yield return null;
            }
            for(float i = 0; i < time; i += Time.deltaTime) {
                item.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(red, Color.black, i / time));
                yield return null;
            }
        }
    }

}
