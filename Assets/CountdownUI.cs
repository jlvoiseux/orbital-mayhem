using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class CountdownUI : MonoBehaviour {

	public Text downText;
	public bool reset = false;
	public VRTK.VRTK_ControllerEvents leftControllerEvents;
	public VRTK.VRTK_ControllerEvents rightControllerEvents;    
	public int increment = 0;
	public GameObject planet;


	// Update is called once per frame
	void Update () {

		if (planet.GetComponent<OrbitMotion>().panelIndex == 10 && increment == 0) {
			increment = (int)Time.timeSinceLevelLoad;

		}

		if (planet.GetComponent<OrbitMotion>().panelIndex >= 10){
			downText.text = (120-(int)Time.timeSinceLevelLoad+increment).ToString();
			if ((120 - (int)Time.timeSinceLevelLoad + increment) <= 0) {
				downText.text = "Time's up !";
			}

		}

	}
}