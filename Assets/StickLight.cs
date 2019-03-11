using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickLight : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Transform>().SetPositionAndRotation(new Vector3(0, 15, 0), new Quaternion(0, 0, 0, 0));
	}
}
