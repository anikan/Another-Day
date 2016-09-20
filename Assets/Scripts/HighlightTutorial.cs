using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class HighlightTutorial {
    public static List<GameObject> grab = new List<GameObject>();
    public static List<GameObject> touchpad = new List<GameObject>();

    private static GameObject triggerObject;
    private static GameObject trackpadObject;

    /// <summary>
    /// Get the trigger and touchpad objects from resources.
    /// </summary>
    public static void setup()
    {
        triggerObject = Resources.Load("Vive Models\\trigger") as GameObject;
        trackpadObject = Resources.Load("Vive Models\\trackpad") as GameObject;
    }

    public static void turnOnGrabHL(GameObject controller) {
        //GameObject x = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        GameObject trigger = GameObject.Instantiate<GameObject>(triggerObject);

        trigger.transform.SetParent(controller.transform);
        trigger.transform.localPosition = new Vector3(0, -.03f, -.04f);
        trigger.transform.localScale = new Vector3(.01f, .01f, .01f);
        trigger.GetComponentInChildren<Renderer>().material = Resources.Load("color") as Material;

        /*
        GameObject light = new GameObject();
        light.transform.SetParent(x.transform);
        Light l = light.AddComponent<Light>();
        l.type = LightType.Spot;
        l.spotAngle = 37;
        l.intensity = 1.6f;
        l.range = .5f;
        light.transform.localPosition = new Vector3(0, -3.5f, -1.6f);
        Quaternion xy = Quaternion.Euler(-68.63f, 0, 0);
        light.transform.localRotation = xy;*/

        grab.Add(trigger);
    }
    public static void turnOffGrabHL() {
        foreach(var item in grab) {
            GameObject.DestroyImmediate(item);
        }
    }

    public static void turnOnTouchPadHL(GameObject controller) {
        for(int i = 0; i < 2; i++) {

            GameObject trackPad = GameObject.Instantiate<GameObject>(trackpadObject);

            trackPad.transform.SetParent(controller.transform);
            trackPad.transform.localPosition = (i == 0 ? new Vector3(.0175f,.0063f, -.0503f) : new Vector3(-.016f, .0063f, -.0503f));
            trackPad.transform.localScale = new Vector3(.01f, .01f, .01f);
            trackPad.GetComponentInChildren<Renderer>().material = (i == 0 ? Resources.Load("color1") as Material: Resources.Load("color") as Material);

            //GameObject light = new GameObject();
            //light.transform.SetParent(x.transform);
            //Light l = light.AddComponent<Light>();
            //l.type = LightType.Spot;
            //l.spotAngle = 25;
            //l.intensity = 4f;
            //l.range = .2f;
            //light.transform.localPosition = new Vector3(0, 2.7f,0);
            //Quaternion xy = Quaternion.Euler(90, 0, 0);
            //light.transform.localRotation = xy;

            touchpad.Add(trackPad);
        }
    }



    public static void turnOffTouchPadHL() {
        foreach(var item in touchpad) {
            GameObject.DestroyImmediate(item);
        }
    }

}
