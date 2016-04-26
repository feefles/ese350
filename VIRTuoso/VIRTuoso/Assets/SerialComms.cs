using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Timers;
using System.Threading;

public class SerialComms : MonoBehaviour {


	public class toRun {

		public void call_at_freq(){
			while (true) {
				int bpm = Wand.bpm;
				Thread.Sleep (60000/bpm);
				mbed.Write ("Y");

			}

		}

	}

	static string send = "Y";


	static System.IO.Ports.SerialPort mbed;

	// Use this for initialization
	void Start () {
		mbed = new SerialPort ("COM3");
		mbed.ReadTimeout = 10000;
		mbed.Open ();
		toRun tr = new toRun ();
		Thread t = new Thread(new ThreadStart(tr.call_at_freq));
		t.Start ();
	
	}


	void call_at_freq(int freq){
		while (true) {
			Thread.Sleep (1000);
			mbed.Write ("Y");

		}

	}
	
	// Update is called once per frame
	void Update () {
		/*

		THIS CODE TO QUERY THE MBED, WE WANT TO DO THE OPPOSITE 

		byte[] buf = new byte[1000];
		int num = mbed.BytesToRead;
		string s = "";
		if(num != 0){
			mbed.Read (buf, 0, num);
		}

		for (int i = 0; i < num; i++) {
			s += (char)buf [i];
		}

		if (num != 0) {
			Debug.Log (s);
		}

		*/

		if (Input.anyKey) {
			//mbed.Write (send);
		}
			
	
	}
}
