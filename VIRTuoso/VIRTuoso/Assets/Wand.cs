using UnityEngine;
using System.Collections;
using System;


public class Wand : MonoBehaviour {
	enum Gesture {
		DOWN=1, UP=2, LEFT=3, RIGHT=4, NONE=0
	};

	public enum Action {
		CRESCENDO=0, DECRESCENDO=1, TEMPOCHANGE=2
	};
	// Use this for initialization
	void Start () {
	
	}
	static int timeSignature = 4; // 4/4 
	public static int bpm = 120;


	// make this public so game controller can evaluate!
	public float[] timeOfLastAction = new float[3];
	public float[] payloadOfLastAction = new float[3];
	public float curBPM = bpm;


	static int numGestures = 4;
	float[] pastYPos = new float[numGestures];
	float[] pastXPos = new float[numGestures];
	float[] lastSwitches = new float[timeSignature];
	float[] prevBpm = new float[timeSignature];

	int tempocount = 0;

	static int pgc = 40;
	Wand.Gesture[] pastGestures = new Wand.Gesture[pgc];
	Wand.Gesture[] lastTransition = new Wand.Gesture[2];
	int poscount = 0;

	int gesturecount = 0;
	bool instrumentSelected = false;

	int selectCounter = 0;
	// Update is called once per frame
	void Update () {
		Instrument[] instruments = GameObject.FindObjectsOfType<Instrument> ();
		pastYPos [poscount % numGestures] = this.transform.localPosition.y;
		pastXPos [poscount % numGestures] = this.transform.localPosition.x;

		Gesture curGesture = GestureRecognize ();
		if (curGesture == Gesture.NONE) {
			curGesture = pastGestures[(poscount - 1 + pgc) % pgc];
		}
		pastGestures[poscount % pgc] = curGesture;

		Boolean selected = false;

		// select instrument
		foreach (Instrument instrument in instruments) {
			if (Math.Abs (this.transform.localPosition.x - instrument.transform.localPosition.x) < 0.3f) {
				if (Math.Abs (this.transform.localPosition.y - instrument.transform.localPosition.y) > 0.8f) {
					if (curGesture == Gesture.RIGHT || curGesture == Gesture.LEFT || curGesture == Gesture.NONE) {
						instrument.Deselect ();
						continue;
					}
				}

				instrument.Select ();

				selected = true;
				tempocount = 0; //stop counting tempo
				if (selectCounter < 10) { // select for 10 frames before volume changes
					selectCounter++;
					return;
				}

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
		if (!selected) {
			//Debug.Log("nothing selected");
			TempoRecognize (); // nothing is selected right now
			selectCounter = 0;
		}
		poscount++;
}


	void TempoRecognize() {
		int mostRecentGesturePointer = ((poscount-1 + pgc) % pgc);
		Gesture[] oldGestures = new Gesture[pgc / 2];
		Gesture [] newGestures = new Gesture[pgc / 2];
		for (int i = 0; i < pgc /2; i++) {
			oldGestures[i] = pastGestures[(mostRecentGesturePointer + i ) % pgc];
			newGestures[i] = pastGestures[(mostRecentGesturePointer - i +pgc ) % pgc];
		}
		Gesture prevGest = mostCommonGesture(oldGestures);
		Gesture newGest = mostCommonGesture(newGestures);
		//Debug.Log ("gestures: " + oldGestures[0] + oldGestures[1] + oldGestures[2]);


		//Gesture newGest = pastGestures[mostRecentGesturePointer];
		//Gesture prevGest = pastGestures[(mostRecentGesturePointer + 1)% pgc];
		if (prevGest != newGest && prevGest != Gesture.NONE && newGest != Gesture.NONE) {
			if (prevGest == lastTransition[0] && newGest == lastTransition[1]) {
				return; // we already counted this transition
			} 
			/*if (prevGest != lastTransition[1]) {
				tempocount = 0;
				return; // this wasn't a tempo transition
			}*/
			lastTransition[0] = prevGest; 
			lastTransition[1] = newGest;
			//Debug.Log ("newGest Time: " + Time.time + " from " + prevGest + " to " + newGest);
			lastSwitches[tempocount++ % timeSignature] = Time.time;
			if (tempocount > timeSignature) { //populated the array
				// get the average bpm
				float acc = 0;
				for (int i = 1; i < timeSignature; i++) {
					acc = acc + (Math.Abs(lastSwitches[i] - lastSwitches[i-1]));
				}
				float average =  acc / (timeSignature - 1);
				prevBpm[tempocount % timeSignature] = 60.0f / average;
				if((tempocount % timeSignature) == 0) {
					float avgBpm = 0;
					for (int i = 0; i < prevBpm.Length; i++) {
						avgBpm = avgBpm + prevBpm[i];
					}
					avgBpm = avgBpm / (prevBpm.Length-1);
					Debug.Log("bpm: "+ avgBpm);
					curBPM = avgBpm;
					float newspeed = avgBpm / bpm;
					Instrument[] instruments = GameObject.FindObjectsOfType<Instrument> ();
					foreach (Instrument instrument in instruments) {
						instrument.switchSpeed(newspeed);
					}
				}
			}
		}

	}

	void VolumeRecognize(Instrument instrument) {
		Gesture curGest = GestureRecognize ();
		if (curGest == Gesture.UP) {
			instrument.VolumeUp (4.0f);
			Debug.Log ("volume up" + instrument.myInstrument);
			timeOfLastAction[(int)Action.CRESCENDO] = Time.time;
			payloadOfLastAction[(int)Action.CRESCENDO] = (int)instrument.myInstrument;
		} else if (curGest == Gesture.DOWN) {
			instrument.VolumeDown(4.0f);
			Debug.Log ("volume down");
			timeOfLastAction[(int)Action.DECRESCENDO] = Time.time;
			payloadOfLastAction[(int)Action.DECRESCENDO] = (int)instrument.myInstrument;
		}
			
		
	}



	Wand.Gesture GestureRecognize() {
		Wand.Gesture[] potentialGestures = new Gesture[numGestures - 1];
		float waywardDeviation = 0.5f;

		int[] count = new int[5];
		for (int i = 1; i < pastYPos.Length; i++) {
			// decreasing and x doesn't change very much
			if (pastYPos [i] > pastYPos [i - 1] && Math.Abs(pastYPos [i] - pastYPos [i - 1]) > 0.05f &&
				Math.Abs (pastXPos [i] - pastXPos [i - 1]) < waywardDeviation) {
				potentialGestures [i - 1] = Wand.Gesture.UP;
				count [(int)Wand.Gesture.UP]++;
			} else if (pastYPos [i] < pastYPos [i - 1] && Math.Abs (pastYPos [i - 1] - pastYPos [i]) > 0.04f &&
				Math.Abs (pastXPos [i] - pastXPos [i - 1]) < waywardDeviation) {
				potentialGestures [i - 1] = Wand.Gesture.DOWN;
				count [(int)Wand.Gesture.DOWN]++;

			} else if (pastXPos [i] < pastXPos [i - 1] && pastXPos [i - 1] - pastXPos [i] > 0.05f &&
				Math.Abs (pastYPos [i] - pastYPos [i - 1]) < waywardDeviation) {
				potentialGestures [i - 1] = Wand.Gesture.LEFT;
				count [(int)Wand.Gesture.LEFT]++;

			} else if (pastXPos [i] > pastXPos [i - 1] && pastXPos [i] - pastXPos [i - 1] > 0.05f &&
				Math.Abs (pastYPos [i] - pastYPos [i - 1]) < waywardDeviation) {
				potentialGestures [i - 1] = Wand.Gesture.RIGHT;
				count [(int)Wand.Gesture.RIGHT]++;

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
//			Debug.Log ((Wand.Gesture) maxIndex);

			return (Wand.Gesture) maxIndex;
		}
	}

	Wand.Gesture mostCommonGesture(Wand.Gesture[] gestures) {
		int[] count = new int[5];
		foreach (Wand.Gesture g in gestures) {
			if (g != Gesture.NONE) {
				count[(int) g]++;
			}
		}
		int maxIndex = 0;
		int maxCount = 0;
		for (int i = 0; i < count.Length; i++) {
			if (count[i] > maxCount) {
				maxCount = count[i];
				maxIndex = i;
			}
		}
		//Debug.Log ("most common: " + (Wand.Gesture) maxIndex);
		return (Wand.Gesture)maxIndex;
	}



}
