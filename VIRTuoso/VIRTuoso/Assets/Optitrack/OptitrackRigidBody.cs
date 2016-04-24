using UnityEngine;
using System.Collections;
using System;
//=============================================================================----
// Author: Bradley Newman - USC Worldbuilding Media Lab - worldbuilding.usc.edu
//=============================================================================----

// This script will read the tracking data from OptitrackRigidBodyManager.cs
// for the rigid body that corresponds to the ID defined in this script.
// Usage: Attach OptitrackRigidBody.cs to an empty Game Object
// and enter the ID number as specified in the Motive > Rigid Body Settings > Advanced > User Data field.
// Requirements:
// 1. Instance of OptitrackRigidBodyManager.cs

public class OptitrackRigidBody : MonoBehaviour {	
	public int ID;
	public int speed = 30;
	private bool foundIndex = false;
	[HideInInspector]
    public int index;

    public bool usePostionTracking = true;
    public bool useRotationTracking = true;

    public GameObject originOverride;
	Quaternion prevRot;
	public float smoothing = 0.1f;

    void Start() {
        //optitrackTransform  = new GameObject().transform;
    }

	void Update () {
		//If we have received a packet from Motive then look for the rigid body ID index
		if(foundIndex == false) 
		{
			if(OptitrackRigidBodyManager.instance.receivedFirstRigidBodyPacket) 
			{
				if(foundIndex == false) 
				{
					for(int i = 0; i < OptitrackRigidBodyManager.instance.rigidBodyIDs.Length; i++) 
					{
						//Looking for ID in array of rigid body IDs
						if(OptitrackRigidBodyManager.instance.rigidBodyIDs[i] == ID) 
						{
							index = i; //Found ID
						}
					}
					foundIndex = true;
				}
			}
		}
		else {
            if (usePostionTracking)
                if (originOverride != null)
                {
					transform.position = (OptitrackRigidBodyManager.instance.rigidBodyPositions[index] - OptitrackRigidBodyManager.instance.origin.position) + originOverride.transform.position;
                }
                else{
					//print("Check");
					Vector3 temp = transform.position;
				    temp.y = ((OptitrackRigidBodyManager.instance.rigidBodyPositions[index].y)*-5.0f); 
					
					temp.x = ((OptitrackRigidBodyManager.instance.rigidBodyPositions[index].x)*-10.0f);
					//temp.z = ((OptitrackRigidBodyManager.instance.rigidBodyPositions[index].z)*-10.0f); 

					transform.position = temp;
				    //float v = OptitrackRigidBodyManager.instance.rigidBodyPositions[index].y * 10;
				    //GetComponent<Rigidbody2D>().velocity = new Vector2(0, v) * speed;
				}
            if (useRotationTracking)
                if (originOverride != null)
                {
                    //Subtract the origin rotation used by OptitrackRigidBodyManager
                    transform.rotation = Quaternion.Inverse(OptitrackRigidBodyManager.instance.origin.rotation) * OptitrackRigidBodyManager.instance.rigidBodyQuaternions[index];
                    //Add the originOverride rotation applied to this rigid body
                    transform.rotation = originOverride.transform.rotation * transform.rotation;
                }
                else {
				if (prevRot == null) {
					prevRot = OptitrackRigidBodyManager.instance.rigidBodyQuaternions[index];
				}
				transform.rotation = Quaternion.Slerp(prevRot, OptitrackRigidBodyManager.instance.rigidBodyQuaternions[index], 0.05f);
				//transform.rotation = smoothing * Quaternion.Slerp(prevRot, OptitrackRigidBodyManager.instance.rigidBodyQuaternions[index], 0.1f) +
				//					(1-smoothing) * prevRot;

				prevRot = transform.rotation;

			}
		}
	}

    public Vector3 GetPostion() {
        return OptitrackRigidBodyManager.instance.rigidBodyPositions[index];
    }

    public Quaternion GetRotationQuaternion() {
        if (foundIndex)
            return OptitrackRigidBodyManager.instance.rigidBodyQuaternions[index];
        else
            return Quaternion.identity;
    }

    public Vector3 GetRotationEuler() {
        if (foundIndex)
            return OptitrackRigidBodyManager.instance.rigidBodyQuaternions[index].eulerAngles;
        else
            return Vector3.zero;
    }
    /*
    public Transform GetTransform() {
        optitrackTransform.position = OptitrackRigidBodyManager.instance.rigidBodyPositions[index];
        optitrackTransform.rotation = OptitrackRigidBodyManager.instance.rigidBodyQuaternions[index];
        return optitrackTransform;
    }*/
}