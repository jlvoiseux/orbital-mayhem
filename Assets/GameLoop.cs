using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//GameLoop.cs
//

//Jean-Louis Voiseux, 29/06/2017


// Main game loop. References and sychonizes most elements in the game.
public class GameLoop : MonoBehaviour {
	public GameObject planet;
	public Transform planetTransform;
	public GameObject ship1;
	public GameObject ship2;
	public GameObject ship3;
	public GameObject ship4;
	public GameObject ship5;
	public GameObject ship6;
	public GameObject ship7;

	public GameObject TheLastOne;

	public GameObject[] shipArray;
	public GameObject currentShip;

	public GameObject mainSystem;
	public float distance = 3f;
	public bool hasDisappeard = true;
	public float timeToReset;
	public GameObject bigParent;
	public Ellipse ellipse;
	public GameObject source;
	public Vector3 previousPosition;

	public int randPos;
	public int randRot;
	public int randShip;

	public int score;

	public GameObject STcDown;
	public GameObject STend;
	public GameObject STworldUI;

	public GameObject SMcDown;
	public GameObject SMend;
	public GameObject SMworldUI;

	public Vector3[] posArray = new Vector3[16];
	public Vector3[] rotArray = new Vector3[15];




	public GameObject leftGrip;
	public VRTK.VRTK_ControllerEvents leftControllerEvents;

	//right controller
	public GameObject rightGrip;
	public VRTK.VRTK_ControllerEvents rightControllerEvents;

	public GameObject panel1;
	public bool panel1bool;
	public GameObject panel2;
	public bool panel2bool;
	public GameObject panel3;
	public bool panel3bool;
	public GameObject panel4;
	public bool panel4bool;
	public GameObject panel6;
	public bool panel6bool;
	public GameObject panel9;
	public bool panel9bool;

	public int panelIndex = 0;
	public GameObject[] panelArray;
	public bool tutorialOn = true;
	public bool isPressed = false;

	// Use this for initialization
	void Start () {

		//Initialize possible ship positions
		panelArray = new GameObject[]{panel1, panel2, panel3, panel4,  panel6, panel9};

		
		posArray [0] = new Vector3 (7f, 1f, 0f);
		posArray [1] = new Vector3 (-7f, 1f, 0f);
		posArray [2] = new Vector3 (6.7f, 1f, 2f);
		posArray [3] = new Vector3 (-6.7f, 1f, 2f);
		posArray [4] = new Vector3 (5.7f, 1f, 4f);
		posArray [5] = new Vector3 (-5.7f, 1f, 4f);
		posArray [6] = new Vector3 (3.6f, 1f, 6f);
		posArray [7] = new Vector3 (-3.6f, 1f, 6f);
		posArray [8] = new Vector3 (0f, 1f, 7f);
		posArray [9] = new Vector3 (2f, 1f, 6.7f);
		posArray [10] = new Vector3 (-2f, 1f, 6.7f);
		posArray [11] = new Vector3 (4f, 1f, 5.7f);
		posArray [12]  = new Vector3 (-4f, 1f, 5.7f);
		posArray [13] = new Vector3 (6f, 1f, 2f);
		posArray [14] = new Vector3 (-6f, 1f, 2f);

		rotArray [0] = new Vector3 (-90f, 0f, 84f);
		rotArray [1] = new Vector3 (-27f, -187f, 491f);
		rotArray [2] = new Vector3 (- 62f, -247f, 516f);
		rotArray [3] = new Vector3 (-68f, -113f, 390f);
		rotArray [4] = new Vector3 (-42f, -234f, 467f);
		rotArray [5] = new Vector3 (-47f, -498f, 349f);
		rotArray [6] = new Vector3 (-1.34f, -958f, 439f);
		rotArray [7] = new Vector3 (-44f, 1060f, 482f);
		rotArray [8] = new Vector3 (-77f, -1054f, 450f);
		rotArray [9] = new Vector3 (12f, -1026f, 324f);
		rotArray [10] = new Vector3 (-80f, -861f, 236f);
		rotArray [11] = new Vector3 (-90f, 0f, 84f);
		rotArray [12] = new Vector3 (-62.1f, 32f, 24f);
		rotArray [13] = new Vector3 (-33f, -45.47f, 63.4f);
		rotArray [14] = new Vector3 (-78f, -833f, 159f);


		shipArray = new GameObject[]{ship1, ship2, ship3, ship4, ship5, ship6, ship7};
		randShip = Random.Range (0, 7);
		currentShip = shipArray [randShip];

		randPos = Random.Range (0, 15);
		randRot = Random.Range (0, 15);

		currentShip.transform.position = posArray [randPos];
		currentShip.transform.eulerAngles = rotArray [randRot];

		if (panelIndex == 10) {
			currentShip.GetComponent<MeshRenderer> ().enabled = true;
		} else {
			currentShip.GetComponent<MeshRenderer> ().enabled = false;
		}

	}


	private void DoButtonPressed(object sender, VRTK.ControllerInteractionEventArgs e)
	{
		if (isPressed == false && panelIndex <6) {
			panelArray [panelIndex].GetComponent<Rigidbody> ().useGravity = true;		
			isPressed = true;
		}
		if (panelIndex == 5) {
			tutorialOn = false;
		}
	}

	private void DoButtonReleased(object sender, VRTK.ControllerInteractionEventArgs e)
	{
		if (isPressed == true && panelIndex != 10) {
			isPressed = false;
			panelIndex = panelIndex + 1;
		}

	}


	public void setPosAndRot(int randPosPrevious){

		randPos = Random.Range (0, 15);

		randRot = Random.Range (0, 15);

		score = score + 1;

		currentShip.transform.position = posArray [randPos];
		currentShip.transform.eulerAngles = rotArray [randRot];

	}
	

	void Update () {
		
		if (tutorialOn == true)
		{				
			rightControllerEvents.TriggerPressed += new VRTK.ControllerInteractionEventHandler(DoButtonPressed);
			rightControllerEvents.TriggerReleased += new VRTK.ControllerInteractionEventHandler(DoButtonReleased);
		}

		if (panelIndex == 10) {
			currentShip.GetComponent<MeshRenderer> ().enabled = true;
			currentShip.GetComponent<ShipCollider> ().enabled = true;
		} else {
			currentShip.GetComponent<MeshRenderer> ().enabled = false;
		}

		// Projectle management
		if (Vector3.Magnitude(planet.transform.position) > distance*Vector3.Magnitude(currentShip.transform.position)){
			if (currentShip.GetComponent<ShipCollider>().isTouched == true){
				
				currentShip.GetComponent<ShipCollider> ().isTouched = false;
				currentShip.GetComponent<MeshRenderer> ().enabled = false;
				currentShip.GetComponent<ShipCollider> ().particle.Stop ();
				currentShip.GetComponent<ShipCollider> ().enabled = false;

				randShip = Random.Range (0, 6);
				currentShip = shipArray [randShip];
				setPosAndRot(randPos);



			}
			hasDisappeard = false;
			timeToReset = Time.timeSinceLevelLoad;
			planet.GetComponent<MeshRenderer> ().enabled = false;
			mainSystem.GetComponent<LineRenderer> ().enabled = true;

			planet = (GameObject)Instantiate (Resources.Load ("Earth"));
			planetTransform = planet.transform;
			planetTransform.parent = mainSystem.transform;
			planet.GetComponent<OrbitMotion> ().leftControllerEvents = leftControllerEvents;
			planet.GetComponent<OrbitMotion> ().rightControllerEvents = rightControllerEvents;
			planet.GetComponent<OrbitMotion> ().system = TheLastOne;
			planet.GetComponent<OrbitMotion> ().sun = mainSystem.transform.GetChild (0).gameObject;
			planet.GetComponent<OrbitMotion> ().orbitPath.sun = mainSystem.transform.GetChild (0).gameObject;
			planet.GetComponent<OrbitMotion> ().orbitRenderer = mainSystem.GetComponent<EllipseRenderer> ();
			planet.GetComponent<OrbitMotion> ().lr = mainSystem.GetComponent<LineRenderer> ();
			STcDown.GetComponent<CountdownUI> ().planet = planet;
			STworldUI.GetComponent<TextManager> ().planet = planet;
			STend.GetComponent<end> ().planet = planet;
			SMcDown.GetComponent<CountdownUI> ().planet = planet;
			SMworldUI.GetComponent<TextManager> ().planet = planet;
			SMend.GetComponent<end> ().planet = planet;
			mainSystem.transform.GetChild (0).gameObject.SetActive (true);
			planet.name = "Earth";
			planet.GetComponent<MeshRenderer> ().enabled = true;

		

		}


		// Destroy the tutorial panels when they fall down
		if (panel9 != null) {
			if (panel9.transform.position.y < -50) {
				Destroy (panel9);
				panelIndex = 10;
			}
		}

		if (panel6 != null) {
			if (panel6.transform.position.y < -10) {
				Destroy (panel6);
			}
		}
		if (panel4 != null) {
			if (panel4.transform.position.y < -10) {
				Destroy (panel4);
			}
		}

		if (panel3 != null) {
			if (panel3.transform.position.y < -10) {
				Destroy (panel3);
			}
		}

		if (panel2 != null) {
			if (panel2.transform.position.y < -10) {
				Destroy (panel2);
			}
		}

		if (panel1 != null) {
			if (panel1.transform.position.y < -10) {
				Destroy (panel1);
			}
		}










		if (Time.timeSinceLevelLoad - timeToReset>1) {
			hasDisappeard = true;
		}



	}
							
}
