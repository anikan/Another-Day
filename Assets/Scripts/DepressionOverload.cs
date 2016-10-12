using UnityEngine;
using System.Collections;

public class DepressionOverload : MonoBehaviour {

    public string[] depressionQuotes;
    private int depressionIndex = 0;
    public Object speechBubblePrefab;
    public float sphereRadius;
    public float bubbleDuration;

    private float startTime;
    private float endTime;

    float prevTime = 0;
    public float timeThresh = 1;

	// Use this for initialization
	void Start () {

	  startTime = Time.time;
	  endTime = startTime + bubbleDuration;

	}

	// Update is called once per frame
	void Update () {

	  if (Time.time >= endTime) {

	    Debug.Log("SET");
	    this.gameObject.SetActive(false);

	  }

	  if (Time.time - prevTime > timeThresh) {
	    prevTime = Time.time;

	    float theta = 0, phi = 0;

	    theta = 2*Mathf.PI*Random.Range(0f, 1f);
	    phi = Mathf.Acos((2f * Random.Range(0f, 1f)) - 1f);

	    Vector3 sphereCoords = new Vector3(
	      Mathf.Cos(theta) * Mathf.Sin(phi),
	      Mathf.Sin(theta) * Mathf.Sin(phi),
	      Mathf.Cos(phi));

	    sphereCoords *= sphereRadius;
	    sphereCoords += transform.position;

	    GameObject textBox = 
	      (GameObject)GameObject.Instantiate(speechBubblePrefab, sphereCoords, this.transform.rotation); 
	    //create bubbl

	    if (depressionIndex >= depressionQuotes.Length) depressionIndex = 0;

	    textBox.GetComponent<TextBubbleScript>().fullMessage =  depressionQuotes[depressionIndex++];
	    }
	  }

	private Vector3 getRandomOnSphere() {

      Vector3 returnVector = new Vector3();

	  float randomX1 = Random.Range(-1f, 1f);
	  float randomX2 = Random.Range(-1f, 1f);

	  Debug.Log("RANDOM: " + randomX1);

	  returnVector.x = 2f * (Mathf.Sqrt(1f - Mathf.Pow(randomX1,2f) - Mathf.Pow(randomX2,2f)));
	  returnVector.y = 2f * (Mathf.Sqrt(1f - Mathf.Pow(randomX1,2f) - Mathf.Pow(randomX2, 2f)));
	  returnVector.z = 1f - (2f * (Mathf.Pow(randomX1,2f) + Mathf.Pow(randomX2,2f)));

	  returnVector *= sphereRadius;
	  returnVector += this.transform.position;

	  return returnVector;

	}
}
