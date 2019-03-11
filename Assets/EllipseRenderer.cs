//
//EllipseRenderer.cs
//

//Jean-Louis Voiseux, 28/06/2017
//Heavily based on the video tutorials "Orbit Paths in Unity: Understanding Ellipses" and "Orbital Paths in Unity, Part 2: Making an Ellipse Class", by Board To Bits Games
//https://www.youtube.com/watch?v=mQKGRoV_jBc
//https://www.youtube.com/watch?v=Or3fA-UjnwU

//Draws an elliptical orbit and adapts it to the movement of the player and its controllers



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(LineRenderer))]
public class EllipseRenderer : MonoBehaviour {

	//main object (the whole system)
	public GameObject source;
	//line generator (draws the orbit)
	public LineRenderer lr;
	//left controller
	public GameObject leftGrip;
    public VRTK.VRTK_ControllerEvents leftControllerEvents;
    
	//right controller
	public GameObject rightGrip;
    public VRTK.VRTK_ControllerEvents rightControllerEvents;    



	//the more segments there are, the rounder the ellipse is
	public int segments = 64;

    public GameObject planet;

	public Ellipse ellipse;
	//initialize the semi great axis
	float a = 0.3f;
    float e = 0.7f;
    float m = 1.3f;








	void Awake(){		
		CalculateEllipse(0.3f, 0.7f); 

	}

 
	//draw the ellipse
	void CalculateEllipse(float a, float e){

		Vector3[] points = new Vector3[segments + 1];
		for(int i=0; i<segments; i++){
			float[] ellipsePoints = ellipse.Evaluate((float)i / (float)segments, a, e);
			Vector2 position2D = new Vector2(ellipsePoints[0], ellipsePoints[1]);
			points [i] = new Vector3 (position2D.x, position2D.y, 0f);
		}
		points [segments] = points [0];

		lr.positionCount = segments + 1;
		lr.SetPositions (points);
	}

	//return the semi  great axis (constantly evolving)
	public float get_a(){
		return a;
	}

    public float get_e()
    {
        return e;
    }

	void Update(){
		//left controller position
		Vector3 leftPos = leftGrip.transform.position;
		//right controller position
		Vector3 rightPos = rightGrip.transform.position;
		//positioning of the whole system at the middle of the controllers
		Vector3 midPoint = (leftPos / 2f) + (rightPos / 2f); 
		//computation the the semi great axis
		Vector3 greatAxis = leftPos - rightPos;
		float a_temp = greatAxis.magnitude / 2f;
		if (a_temp > 0.1f) {
			a = a_temp;   
		} 
		else {
			a = 0.1f;
		}
		//move the whole system depending on the position of the middle of the controllers
		source.transform.position = midPoint;
		//rotate the ellipse to give the feeling that the orbit sticks to the controllers
		//source.transform.rotation = Quaternion.LookRotation(Vector3.Cross(greatAxis, Vector3.up));																																																																																													
		source.transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(Vector3.Cross(greatAxis, new Vector3(0f, 1f, 0f)), Vector3.up));
        


        if (rightControllerEvents.IsButtonPressed(VRTK.VRTK_ControllerEvents.ButtonAlias.TouchpadPress) && m <= 5f){
            m=m+0.05f;
            planet.transform.localScale = planet.transform.localScale+Vector3.one*0.0005f;
        }
        if (leftControllerEvents.IsButtonPressed(VRTK.VRTK_ControllerEvents.ButtonAlias.TouchpadPress) && m >= 1.02f)
        {
            m = m - 0.05f;
            planet.transform.localScale = planet.transform.localScale - Vector3.one * 0.0005f;
        }
        e = (Mathf.Abs(1-(1/m)));        
		CalculateEllipse (a,e);
        
	}
}
