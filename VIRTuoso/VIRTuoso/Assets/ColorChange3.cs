﻿using UnityEngine;
using System.Collections;

public class ColorChange3 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MeshRenderer r = gameObject.GetComponent<MeshRenderer> ();
		if (r == null) {
			Debug.Log ("no renderr");
		}

		GlobalColorCoordinatorHook.rend [2] = r;
	}

	// Update is called once per frame
	void Update () {


	}
}
