using UnityEngine;
using System.Collections;

public class Instrument : MonoBehaviour {
	AudioSource[] aSources;
	float[] speeds;
	int curSpeed;

	// Use this for initialization
	void Start () {
		aSources = GetComponents<AudioSource>();
		float[] set_speeds = {0.8f, 1.0f, 1.2f};
		speeds = set_speeds;

		float startingSpeed = 0.8f;
		curSpeed = 0;


		switchSpeed (curSpeed, curSpeed + 1 );
	}

	void switchSpeed(int newCursor, int prevCursor) {
		float newSpeed = speeds [newCursor];
		for (int i = 0; i < aSources.Length; i++) {
			aSources [i].pitch = newSpeed / speeds [i];
			if (speeds [i] == newSpeed) {
				aSources[i].mute = false;
			} else {
				aSources [i].mute = true;
			}
		}
//		float targetVolume = aSources [newCursor].volume;
//		aSources [newCursor].volume = 0;
//		aSources [newCursor].mute = false;
//		while (aSources [newCursor].volume != targetVolume) {
//			aSources [newCursor].volume = aSources [newCursor].volume + 0.01f;
//			aSources [prevCursor].volume = aSources [prevCursor].volume - 0.01f;
//		}
//		aSources [prevCursor].mute = true;

	}

		
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			int formerSpeed = curSpeed;
			curSpeed = Mathf.Min (curSpeed + 1, speeds.Length -1);
			switchSpeed (curSpeed, formerSpeed);

			Debug.Log ("Increase " + curSpeed);
		}	
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			int formerSpeed = curSpeed;
			curSpeed = Mathf.Max (curSpeed - 1, 0);
			switchSpeed (curSpeed, formerSpeed);
			Debug.Log ("Decrease " + curSpeed);
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			foreach (AudioSource s in aSources) {
				s.volume = s.volume - .1f;
			}
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			foreach (AudioSource s in aSources) {
				s.volume = s.volume + .1f;
			}
		}


	}


	public void VolumeUp(float magnitude) {
		foreach (AudioSource s in aSources) {
			s.volume = s.volume + magnitude * .1f;
		}
	}
	public void VolumeDown(float magnitude) {
		foreach (AudioSource s in aSources) {
			s.volume = s.volume - magnitude * .1f;
		}
	}

	// defines this as the selected instrument
	public void Select() {
		this.transform.GetComponent<Renderer>().material.color = Color.red;
	}
	public void Deselect() {
		this.transform.GetComponent<Renderer>().material.color = Color.white;
	}
}
