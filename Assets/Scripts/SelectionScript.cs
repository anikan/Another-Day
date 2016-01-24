using UnityEngine;
using System.Collections;

public class SelectionScript : MonoBehaviour {
    ActivatableObject activatedObject;
    public GameObject reticle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Tap"))
        {
            //RaycastHit hit;
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            /*if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                this.GetComponent<Renderer>().material = mat2;*/

            Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.forward * 100);

            //Check if there is an object directly ahead and that it has the activate script.
            RaycastHit hitInfo;
            
            Physics.Raycast(transform.position, transform.forward, out hitInfo, 1000);

            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 1000) && hitInfo.transform.gameObject.GetComponent<ActivatableObject>() != null)
            {
                if (hitInfo.transform.gameObject != activatedObject && activatedObject != null)
                {
                    activatedObject.Deactivate();
                }

                hitInfo.transform.gameObject.GetComponent<ActivatableObject>().Activate();
                activatedObject = hitInfo.transform.gameObject.GetComponent<ActivatableObject>();
            }

            else if (activatedObject != null)
            {
                activatedObject.Deactivate();
                activatedObject = null;
            }
        }

        else if (activatedObject != null)
        {
            activatedObject.Deactivate();
            activatedObject = null;
        }
    }
}
