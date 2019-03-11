using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	public GameObject planet;
	
	// Update is called once per frame
	void Update () {
		planet.transform.Rotate (0, 0, 20f * Time.deltaTime);
	}
}
