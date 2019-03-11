using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

	public GameObject planet;
	public Text text1;
	public Text text2;
	public Text text3;

	// Use this for initialization
	void Start () {
		text1.enabled = false;
		text2.enabled = false;
		text3.enabled = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (planet.GetComponent<OrbitMotion> ().panelIndex == 10) {
			text1.enabled = true;
			text2.enabled = true;
			text3.enabled = true;
		}
	}
}
