using UnityEngine;
using System.Collections;
using System;

// this class will control the logic for the conductor-hero like game 

public class GameLogic : MonoBehaviour {
	public float[] KeyTimes = new float[10];
	public Wand.Action[] KeyActions = new Wand.Action[10];
	public int[] PayLoad = new int[10];

	public int actionPointer = 0;
	public float actionTimeout = 3.0f; // you have 3 seconds to act
	public int numMissed = 0;
	Wand wand;

	// okay  here we go...
	public enum Instrument {
		violin1=0, violin2=1, viola=2, cello=3, bass=4, winds=5, brass=6, percussion=7 
	}


	// Use this for initialization
	void Start () {
		wand = GameObject.FindObjectOfType<Wand> ();
		// make instrument 5 crescendo at time 10
		KeyTimes [0] = 3.0f;
		KeyActions [0] = Wand.Action.CRESCENDO;
		PayLoad [0] = (int) Instrument.bass; 

		// make instrument 3 decrescendo at time 15
		KeyTimes [1] = 15.0f;
		KeyActions [1] = Wand.Action.DECRESCENDO;
		PayLoad [1] = (int) Instrument.viola; 

		// do a tempo change at time 30
		KeyTimes [2] = 30f;
		KeyActions [2] = Wand.Action.TEMPOCHANGE;
		PayLoad [2] = 100; 
	}
	
	// Update is called once per frame
	void Update () {
		if (actionPointer >= KeyTimes.Length) {
			return;
		}
		// are we on the current action?
		if (Math.Abs (Time.time - KeyTimes [actionPointer]) < .01) {
			// handle LED code to send signal appropriately
			Debug.Log("Do Action: " + KeyActions[actionPointer]);
		}
		// now we wait for their response
		if (Math.Abs (Time.time - (float)KeyTimes [actionPointer]) < actionTimeout) {
			switch (KeyActions[actionPointer]) {
			case Wand.Action.CRESCENDO:
				// they got it
				if ((Time.time - wand.timeOfLastAction[(int)Wand.Action.CRESCENDO]) < actionTimeout &&
				    (int) wand.payloadOfLastAction[(int)Wand.Action.CRESCENDO] == PayLoad[actionPointer]) {
					actionPointer++;
					// maybe some LED feedback here for correct
					Debug.Log("You got the crescendo!");
				}
				break;
			case Wand.Action.DECRESCENDO:
				// they got it
				if ((Time.time - wand.timeOfLastAction[(int)Wand.Action.DECRESCENDO]) < actionTimeout &&
				    (int) wand.payloadOfLastAction[(int)Wand.Action.DECRESCENDO] == PayLoad[actionPointer]) {
					actionPointer++;
					// maybe some LED feedback here for correct
					Debug.Log("You got the decrescendo!");

				}
				break;
			case Wand.Action.TEMPOCHANGE:
				if (Math.Abs (PayLoad[actionPointer] - wand.curBPM ) < 5) { // within 5 bpm of our request
					actionPointer++;
					Debug.Log("You got the tempo change!");

				}
				break;
			default:
				return;
			}
		} 
		else if (Time.time >  KeyTimes [actionPointer] && Time.time - KeyTimes [actionPointer] < actionTimeout + .5 ) {
			actionPointer++;
			numMissed++; 
			// do some LED action here to indicate they missed it :( 
			Debug.Log ("You missed it!");
		}
	}
}
