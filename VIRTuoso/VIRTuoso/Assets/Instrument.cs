using UnityEngine;
using System.Collections;

public class Instrument : MonoBehaviour {
	AudioSource[] aSources;
	float[] speeds;
	int curSpeed;

	// Use this for initialization
	void Start () {
		aSources = GetComponentsInChildren<AudioSource>();
		float[] set_speeds = {0.4f, 0.6f, 0.8f, 1.0f, 1.2f, 1.4f, 1.6f};
		speeds = set_speeds;

		float startingSpeed = 1.0f;
		curSpeed = 3;
		for (int i = 0; i < aSources.Length; i++) {
			aSources [i].pitch = startingSpeed / speeds [i];
			aSources [i].mute = true;
			aSources[i].loop = true;
		} 
		aSources [curSpeed].mute = false;

		//switchSpeed (curSpeed, curSpeed + 1 );
	}

	void switchSpeed(int newCursor, int prevCursor) {
		float newSpeed = speeds [newCursor];
		for (int i = 0; i < aSources.Length; i++) {
			aSources [i].pitch = newSpeed / speeds [i];
			if (speeds [i] == newSpeed) {
				aSources[i].mute = true;
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

	public void switchSpeed(float newSpeed) {
		// first find the closest speed
		float mindiff = 100;
		float closestSpeed = 1.0f;
		for (int i = 0; i < speeds.Length; i++) {
			if (Mathf.Abs(speeds[i] - newSpeed) < mindiff) {
				mindiff = Mathf.Abs(speeds[i] - newSpeed);
				closestSpeed = speeds[i];
			}
		}
		newSpeed = closestSpeed;
		for (int i = 0; i < aSources.Length; i++) {
			aSources [i].pitch = newSpeed / speeds [i];
			if (speeds [i] == newSpeed) {
				aSources[i].mute = false;
			} else {
				aSources [i].mute = true;
			}
		}
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
		this.transform.localScale = this.transform.localScale * (aSources [0].volume) + new Vector3(.5f, .5f, .5f);
	}

	public void VolumeDown(float magnitude) {
		foreach (AudioSource s in aSources) {
			s.volume = s.volume - magnitude * .1f;
		}
		this.transform.localScale = this.transform.localScale * (aSources [0].volume) + new Vector3(.5f, .5f, .5f);
	}

	// defines this as the selected instrument
	public void Select() {
		this.transform.GetComponent<Renderer>().material.color = Color.red;
	}
	public void Deselect() {
		this.transform.GetComponent<Renderer>().material.color = Color.white;
	}
}
