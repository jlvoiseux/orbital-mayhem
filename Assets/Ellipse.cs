//
//Ellipse.cs
//

//Jean-Louis Voiseux, 28/06/2017
//Heavily based on the video tutorials "Orbit Paths in Unity: Understanding Ellipses" and "Orbital Paths in Unity, Part 2: Making an Ellipse Class", by Board To Bits Games
//https://www.youtube.com/watch?v=mQKGRoV_jBc
//https://www.youtube.com/watch?v=Or3fA-UjnwU

//An ellipse ; drawn in order to obtain as many relevant quantities as possible (featured in Kepler 1sr law)



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ellipse {	
	
	
	//focus of the ellipsse
	public GameObject sun;
	float[] ellipsePoints;
	public Ellipse(){		
		
	}

	// t : segments, a : semi great axis
	public float[] Evaluate(float t, float a, float e){
		// focal distance
		float c = a * e;
		// semi small axis
		float b = Mathf.Sqrt(a * a - c * c);
		//semi latus rectum
		float p = b * b / a;
		//angle  (rad)
		float angle = (Mathf.Deg2Rad * 360f * t);
		//radius (kepler 1st, WIKI EN)
		float r = p / (1 + e * Mathf.Cos (angle-Mathf.PI/2)); 
		//points commputation
		float x = Mathf.Sin(angle)*a;
		float y = Mathf.Cos (angle) * b;
		ellipsePoints =  new float[]{x, y, a, b, c, e, p, r, angle};
		//keep the sun at a fixed location
		sun.transform.localPosition = new Vector3(c, 0, 0);
		return ellipsePoints;
	}
}
