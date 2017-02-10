using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class ModelOrientation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

    // Update is called once per frame
    void Update() {
		transform.Rotate(0, Input.GetAxis("Horizontal")*45*Time.deltaTime, 0);
    }

	// Offset angle
    public void Offset(Quaternion offsetQuaternion) {
		transform.eulerAngles = new Vector3(0,15,0);
		transform.Rotate(offsetQuaternion.eulerAngles);
    }

}
