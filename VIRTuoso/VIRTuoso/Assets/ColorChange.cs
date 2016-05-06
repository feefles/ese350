using UnityEngine;
using System.Collections;

public class ColorChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MeshRenderer r = gameObject.GetComponent<MeshRenderer> ();
		if (r == null) {
			Debug.Log ("no renderer");
		}

		GlobalColorCoordinatorHook.rend [0] = r;
	}
	
	// Update is called once per frame
	void Update () {
		
	

	}
}
