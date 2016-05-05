using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Timers;
using System.Threading;

public class SerialComms : MonoBehaviour {


	public class toRun {

		public static int bpm = 60;
		public static int sec = 0;
		public static int cnt = 0;

		public int[] inst = { 1, 1, 2, 3, 4 };
		public int[] vol = { 1, 0, 1, 0, 1 };


		public void call_at_freq(){
			while (true) {
				Thread.Sleep (60000/bpm);



				string s = "";
				s += inst [cnt];
				s += vol [cnt];


				mbed.Write (s);

				if (cnt == 4)
					return;
				cnt++;
			}

		}

	}
		

	static System.IO.Ports.SerialPort mbed;

	// Use this for initialization
	void Start () {
		mbed = new SerialPort ("/dev/tty.usbmodem1412");
		mbed.ReadTimeout = 10000;
		mbed.Open ();
		toRun tr = new toRun ();
		Thread t = new Thread(new ThreadStart(tr.call_at_freq));
		t.Start ();
	
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
