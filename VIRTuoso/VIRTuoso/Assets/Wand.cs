using UnityEngine;
using System.Collections;
using System;


public class Wand : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	float[] pastYPos = {0.0f, 0.0f, 0.0f, 0.0f, 0.0f};
	int poscount = 0;

	int gesturecount = 0;
	// Update is called once per frame
	void Update () {
		Instrument instrument = GameObject.FindObjectOfType<Instrument> ();
		Debug.Log (instrument);
		pastYPos [poscount++ % 5] = this.transform.localPosition.y;
		if (this.transform.localPosition.x - instrument.transform.localPosition.x < 0.08f) {
			Debug.Log ("Pointing");
			instrument.Select ();
			// do the gesture recognition here
			if (gesturecount == pastYPos.Length) {
				GestureRecognize(instrument);
				gesturecount = 0;
			} else {
				gesturecount++;
			}

		} else {
			instrument.Deselect();
		}
	}

	void GestureRecognize(Instrument instrument) {
		for (int i = 1; i < pastYPos.Length; i++) {
			if (pastYPos[i] > pastYPos[i-1] && pastYPos[i] - pastYPos[i-1] > 0.1f) {
				instrument.VolumeDown(1.0f);
				Debug.Log ("volume down");
			}
			else if (pastYPos[i] < pastYPos[i-1] && pastYPos[i-1] - pastYPos[i] > 0.1f) {
				instrument.VolumeUp(1.0f);
				Debug.Log ("volume up");
			}
		}
	}



}
