using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class end : MonoBehaviour {

	public Text downText;
	public bool reset = false;
	public VRTK.VRTK_ControllerEvents leftControllerEvents;
	public VRTK.VRTK_ControllerEvents rightControllerEvents;    
	public GameObject planet;
	public int increment = 0;


	// Update is called once per frame
	void Update () {
		
			if (planet.GetComponent<OrbitMotion> ().panelIndex == 10 && increment == 0) {
				increment = (int)Time.timeSinceLevelLoad;

			}
			if (planet.GetComponent<OrbitMotion> ().panelIndex >= 10) {
				if ((120 - (int)Time.timeSinceLevelLoad + increment) <= 0) {
					downText.text = "Thank you for playing !\nLearn more about us on Altheria-Solutions.com\nPress Trigger to reset the game.";
					reset = true;		
				}
			}
			if ((rightControllerEvents.IsButtonPressed (VRTK.VRTK_ControllerEvents.ButtonAlias.TriggerPress) || (leftControllerEvents.IsButtonPressed (VRTK.VRTK_ControllerEvents.ButtonAlias.TriggerPress))) && reset == true) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
			}

	}
}