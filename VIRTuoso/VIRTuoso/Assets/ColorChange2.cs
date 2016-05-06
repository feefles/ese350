using UnityEngine;
using System.Collections;

public class ColorChange2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MeshRenderer r = gameObject.GetComponent<MeshRenderer> ();
		if (r == null) {
			Debug.Log ("r was null");
		}


		GlobalColorCoordinatorHook.rend [1] = r;
	}


	// Update is called once per frame
	void Update () {


	}
}
