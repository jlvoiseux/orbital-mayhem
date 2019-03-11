using UnityEngine.UI;
using UnityEngine;

public class Countdown : MonoBehaviour {
	public float CountdownFrom;
	public Text textbox;

	void Update() {
		float time = CountdownFrom - Time.timeSinceLevelLoad;
		textbox.text = "Time left: " + time.ToString("0.00") + "s";

		if(time<=0f) {
			TimeUp();
		}
	}

	void TimeUp() {
		// this function is called when the timer runs out
	}
}