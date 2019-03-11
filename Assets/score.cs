using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class score : MonoBehaviour {

	public GameObject source;
	public Text scoreText;
	// Update is called once per frame
	void Update () {
		scoreText.text = source.GetComponent<GameLoop> ().score.ToString ();
	}
}
