using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class HighlightTutorial {
    public static List<GameObject> grab = new List<GameObject>();
    public static List<GameObject> touchpad = new List<GameObject>();


    public static void turnOnGrabHL(GameObject controller) {


        GameObject x = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        x.transform.SetParent(controller.transform);
        x.transform.localPosition = new Vector3(0, -.03f, -.04f);
        x.transform.localScale = new Vector3(.01f, .01f, .01f);
         x.GetComponent<Renderer>().material = Resources.Load("color") as Material;
        GameObject light = new GameObject();
        light.transform.SetParent(x.transform);
        Light l = light.AddComponent<Light>();
        l.type = LightType.Spot;
        l.spotAngle = 37;
        l.intensity = 1.6f;
        l.range = .5f;
        light.transform.localPosition = new Vector3(0, -3.5f, -1.6f);
        Quaternion xy = Quaternion.Euler(-68.63f, 0, 0);
        light.transform.localRotation = xy;


        grab.Add(x);
    }
    public static void turnOffGrabHL() {
        foreach(var item in grab) {
            GameObject.DestroyImmediate(item);
        }
    }

    public static void turnOnTouchPadHL(GameObject controller) {
        GameObject x = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        x.transform.SetParent(controller.transform);
        x.transform.localPosition = new Vector3(0.03f, .03f, 0);
        x.transform.localScale = new Vector3(.01f, .01f, .01f);
        x.GetComponent<Renderer>().material = Resources.Load("color") as Material;
        GameObject light = new GameObject();
        light.transform.SetParent(x.transform);
        Light l = light.AddComponent<Light>();
        l.type = LightType.Spot;
        l.spotAngle = 37;
        l.intensity = 1.6f;
        l.range = .5f;
        light.transform.localPosition = new Vector3(0, -3.5f, -1.6f);
        Quaternion xy = Quaternion.Euler(-68.63f, 0, 0);
        light.transform.localRotation = xy;
    }



    public static void turnOffTouchPadHL() {
        foreach(var item in touchpad) {
            GameObject.DestroyImmediate(item);
        }
    }

}
