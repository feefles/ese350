using UnityEngine;
using System.Collections;

public class GlobalColorCoordinatorHook : MonoBehaviour {

	public static MeshRenderer[] rend = new MeshRenderer[4];
	int loop = 20;
	int curr = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		GameObject trumpet = GameObject.Find ("Trumpet");

		if (trumpet != null) {
			trumpet.GetComponent<Renderer> ().material.color = Color.blue;
		} else {
			Debug.Log ("size was 0");
		}

		


	
	}
}
