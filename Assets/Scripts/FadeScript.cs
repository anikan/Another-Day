using UnityEngine;
using System.Collections;

public class FadeScript : MonoBehaviour {

    public float fadeDuration;

	// Use this for initialization
	void Start () {

	  this.GetComponent<MeshRenderer>().material.color.a = 0;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void fadeOut() {

	  StartCoroutine(fade());

	}

	IEnumerator fade() {

	  while (this.GetComponent<MeshRenderer>().material.color.a < 1) {
			
		Color tempColor = this.GetComponent<MeshRenderer>().material.color;
		tempColor.a+=0.01f;
			
		this.GetComponent<MeshRenderer>().material.SetColor("_Color", tempColor);
		
		yield return null;
			
	  }

	  Application.LoadLevel(1);

	}
}
