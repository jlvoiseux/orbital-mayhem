//
//OrbitMotion.cs
//

//Jean-Louis Voiseux, 29/06/2017
//Heavily based on the video tutorial "Orbital Paths in Unity, Part 3: Making a Planet Orbit", by Board To Bits Games
//https://www.youtube.com/watch?v=lKfqi52PqHk

//An orbit path : makes a planet follow the path defined by EllipseRenderer; adapts the planet speed depending on the dimensions of the orbit and the position of the planet relative to the sun (Kepler 2nd and 3st)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMotion : MonoBehaviour {

	//planet
	public GameObject system;
    public Transform orbitingObject;
	//orbit path
    public Ellipse orbitPath;
	//orbit renderer
    public EllipseRenderer orbitRenderer;
    public LineRenderer lr;
    public GameObject sun;
	public GameObject source;
	//gravitational constant
	public float mu = 1f;
	//proportionnality constant orbit size/periode (kepler 3rd)
	public float kepler3constant = 1f;
	public Rigidbody rigid;
    public VRTK.VRTK_ControllerEvents leftControllerEvents;
    public VRTK.VRTK_ControllerEvents rightControllerEvents;
	public int panelIndex;





	 



	//position of the planet on its orbit
	[Range(0f, 1f)]
    public float orbitProgress = 0f;
    public Vector3 previousPosition = Vector3.zero;

    public bool fire = false;
    public bool fire2 = false;
    public Vector3 direction = Vector3.zero;
    public float r = 0f;




    
	//orbit initialization
    public bool orbitActive = true;
    void Start(){

		rigid.useGravity = false;
        if (orbitingObject == null){
            orbitActive = false;
            return;
        }
        float[] orbitPos = orbitPath.Evaluate(orbitProgress, orbitRenderer.get_a(), orbitRenderer.get_e());  
        SetOrbitingObjectPosition(orbitPos);
        StartCoroutine(AnimateOrbit());
    }

	void Update(){
		if (Vector3.Magnitude (source.transform.position) > 20) {
			Destroy(source);
		}
	}

	//positioning of the planet on the orbit
	void SetOrbitingObjectPosition(float[] orbitPos){
        previousPosition = orbitingObject.transform.position;
        orbitingObject.localPosition = new Vector3(orbitPos[0], orbitPos[1], 0);
    }

	public bool getFire(){
		return fire;
	}

	public void reset(){
		orbitActive = false;
		float[] orbitPos = orbitPath.Evaluate(orbitProgress, orbitRenderer.get_a(), orbitRenderer.get_e());  
		SetOrbitingObjectPosition(orbitPos);
		fire = false;
		orbitActive = true;
		StartCoroutine(AnimateOrbit());
	}




    IEnumerator AnimateOrbit(){        
        
        while (orbitActive){


			panelIndex = system.GetComponent<GameLoop> ().panelIndex;
			//if the whole thing goes to hell...
			if (float.IsNaN(orbitProgress)) {
				orbitProgress = 0f;//...bring it back to the living
			}
			orbitingObject.transform.Rotate (200f * Time.deltaTime, 0, 0);
			//dynamically compute the characteristics of the path (kepler 1st)
            float[] orbitPos = orbitPath.Evaluate(orbitProgress, orbitRenderer.get_a(), orbitRenderer.get_e());
			//compute the speed depending on the distance to the sun and the width of the orbit (kepler 2nd, 3st)
			float orbitSpeed = Mathf.Sqrt(mu*((2/orbitPos[7])-(1/orbitPos[2])))*kepler3constant*Mathf.Sqrt(1/(orbitRenderer.get_a()*orbitRenderer.get_a()*orbitRenderer.get_a()));



		

			if ((rightControllerEvents.IsButtonPressed(VRTK.VRTK_ControllerEvents.ButtonAlias.TriggerPress) || (leftControllerEvents.IsButtonPressed(VRTK.VRTK_ControllerEvents.ButtonAlias.TriggerPress))) && fire == false && panelIndex == 10)
            {
                fire = true;

				direction = (orbitingObject.transform.position - previousPosition) / Vector3.Magnitude(orbitingObject.transform.position - previousPosition);
                r = orbitSpeed / 20f;
                lr.enabled = false;
                sun.active = false;
				//rigid.useGravity = true;
				orbitingObject.transform.parent = null;

            }
                

			//the planet is now rotating
            else if (fire == false){
				orbitProgress += Time.deltaTime * orbitSpeed/50f;
                orbitProgress %= 1f;
			    SetOrbitingObjectPosition(orbitPos);

                yield return null;
            }
            else{
                orbitingObject.transform.position += Time.deltaTime *r* direction;
                yield return null;
            }
        }
    }
}
