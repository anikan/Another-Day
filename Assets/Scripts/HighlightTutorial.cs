using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HighlightTutorial:MonoBehaviour {

    public static HighlightTutorial instance;
    public  List<GameObject> grab = new List<GameObject>();
    public  List<GameObject> touchpad = new List<GameObject>();



    void Awake() {
        instance = this;
        //print("awake");
    
        //print(touchpad.Count);
      
    }
    
    public void turnOnGrabHL(GameObject controller) {
        foreach(GameObject item in grab) {
            item.SetActive(true);
            StartCoroutine(GreenLerp(item));
        }
    }
    public  void turnOffGrabHL() {
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
                        StartCoroutine(GreenLerp(items.gameObject));
                    }
                    
                    break;
                case "rightTag":
                    foreach(Renderer items in mats) {
                        StartCoroutine(RedLerp(items.gameObject));
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
    IEnumerator GreenLerp(GameObject item) {
        while(true) {
            
            for(float i = 0; i < time; i += Time.deltaTime) {
                item.GetComponentInChildren<Renderer>().material.SetColor("_EmissionColor",  Color.Lerp(Color.black, Color.green, i / time));
                //print("LERPING");
                yield return null;
            }
            for(float i = 0; i < time; i += Time.deltaTime) {
                //print("LERPING");
                item.GetComponentInChildren<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.green, Color.black, i / time));
                yield return null;
            }
           
        } 
    }
    IEnumerator RedLerp(GameObject item) {
        while(true) {
            for(float i = 0; i < time; i += Time.deltaTime) {
                item.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.black, Color.red, i / time));
                yield return null;
            }
            for(float i = 0; i < time; i += Time.deltaTime) {
                item.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.red, Color.black, i / time));
                yield return null;
            }
        }
    }

}
