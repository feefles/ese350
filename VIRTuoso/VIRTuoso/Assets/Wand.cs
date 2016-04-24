using UnityEngine;
using System.Collections;
using System;


public class Wand : MonoBehaviour {
	enum Gesture {
		DOWN=1, UP=2, LEFT=3, RIGHT=4, NONE=0
	}; 
	// Use this for initialization
	void Start () {
	
	}

	static int numGestures = 5;
	float[] pastYPos = new float[numGestures];
	float[] pastXPos = new float[numGestures];
	int poscount = 0;

	int gesturecount = 0;
	bool instrumentSelected = false;
	// Update is called once per frame
	void Update () {
		Instrument[] instruments = GameObject.FindObjectsOfType<Instrument> ();
		pastYPos [poscount++ % numGestures] = this.transform.localPosition.y;
		pastXPos [poscount++ % numGestures] = this.transform.localPosition.x;
		GestureRecognize ();

		foreach (Instrument instrument in instruments) {
			if (Math.Abs(this.transform.localPosition.x - instrument.transform.localPosition.x) < 0.4f) {
				Debug.Log ("Pointing");
				instrument.Select ();
				instrumentSelected = true;
				// do the gesture recognition here
				if (gesturecount == pastYPos.Length) {
					VolumeRecognize (instrument);
					gesturecount = 0;
				} else {
					gesturecount++;
				}

			} else {
				instrument.Deselect ();
				instrumentSelected = false;
			}
		}
	}


	void TempoRecognize() {
		if (!this.instrumentSelected) {
			Gesture curGest = GestureRecognize();
			
		}
	}

	void VolumeRecognize(Instrument instrument) {
		Gesture curGest = GestureRecognize ();
		if (curGest == Gesture.UP) {
			instrument.VolumeUp (2.0f);
			//Debug.Log ("volume up");
		} else if (curGest == Gesture.DOWN) {
				instrument.VolumeDown(2.0f);
			//	Debug.Log ("volume down");
		}
			
		
	}




	Wand.Gesture GestureRecognize() {
		Wand.Gesture[] potentialGestures = new Gesture[numGestures - 1];

		int[] count = new int[5];
		for (int i = 1; i < pastYPos.Length; i++) {
			// decreasing and x doesn't change very much
			if (pastYPos [i] > pastYPos [i - 1] && pastYPos [i] - pastYPos [i - 1] > 0.01f &&
				Math.Abs (pastXPos [i] - pastXPos [i - 1]) < 0.2f) {
				potentialGestures [i - 1] = Wand.Gesture.DOWN;
				count [(int)Wand.Gesture.DOWN]++;
			} else if (pastYPos [i] < pastYPos [i - 1] && pastYPos [i - 1] - pastYPos [i] > 0.01f &&
				Math.Abs (pastXPos [i] - pastXPos [i - 1]) < 0.2f) {
				potentialGestures [i - 1] = Wand.Gesture.UP;
				count [(int)Wand.Gesture.UP]++;

			} else if (pastXPos [i] < pastXPos [i - 1] && pastXPos [i - 1] - pastXPos [i] > 0.01f &&
				Math.Abs (pastYPos [i] - pastYPos [i - 1]) < 0.2f) {
				potentialGestures [i - 1] = Wand.Gesture.RIGHT;
				count [(int)Wand.Gesture.RIGHT]++;

			} else if (pastXPos [i] > pastXPos [i - 1] && pastXPos [i] - pastXPos [i - 1] > 0.01f &&
				Math.Abs (pastYPos [i] - pastYPos [i - 1]) < 0.2f) {
				potentialGestures [i - 1] = Wand.Gesture.LEFT;
				count [(int)Wand.Gesture.LEFT]++;

			} else {
				potentialGestures [i - 1] = Wand.Gesture.NONE;
				count [(int)Wand.Gesture.NONE]++;

			}
		}
		/*int fudge = 2; 
		int mismatch = 0;

		Wand.Gesture recognizedGesture = potentialGestures[2]; // pick median element arbitrarily
		for (int i = 0; i < potentialGestures.Length; i++) {
			if (potentialGestures[i] != recognizedGesture) {
				mismatch++;
			}
		}
		if (mismatch > fudge) {
			return recognizedGesture;
		}
		return recognizedGesture;
		*/
		int maxIndex = 0;
		int maxCount = 0;
		for (int i = 0; i < count.Length; i++) {
			if (count[i] > maxCount) {
				maxCount = count[i];
				maxIndex = i;
			}
		}
		if (maxCount < numGestures / 2.0) {
			return Wand.Gesture.NONE;
		} else {
			Debug.Log ((Wand.Gesture) maxIndex);

			return (Wand.Gesture) maxIndex;
		}
	}



}
