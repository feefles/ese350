using UnityEngine;
using System.Collections;

public class Instrument : MonoBehaviour {
	AudioSource normalSpeed;
	AudioSource slowSpeed;
	AudioSource fastSpeed;

	AudioSource[] aSources;

	// Use this for initialization
	void Start () {
		aSources = GetComponents<AudioSource>();
		normalSpeed = aSources [0];
		slowSpeed = aSources [1];
		fastSpeed = aSources [2];


		slowSpeed.pitch = 1.0f;
		normalSpeed.pitch = 0.8f;
		fastSpeed.pitch = .666667f;
		slowSpeed.mute = false;
		fastSpeed.mute = true;
		normalSpeed.mute = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			slowSpeed.pitch = 1.0f;
			fastSpeed.mute = true;
			normalSpeed.mute = true;
			slowSpeed.mute = false;
		}	
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			fastSpeed.pitch = 1.0f;
			fastSpeed.mute = false;
			normalSpeed.mute = true;
			slowSpeed.mute = true;
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
}
