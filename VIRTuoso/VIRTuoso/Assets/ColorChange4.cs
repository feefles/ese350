using UnityEngine;
using System.Collections;

public class ColorChange4 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MeshRenderer r = gameObject.GetComponent<MeshRenderer> ();

		GlobalColorCoordinatorHook.rend [3] = r;
	}

	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Renderer> ().material.color = Color.black;

	}
}
