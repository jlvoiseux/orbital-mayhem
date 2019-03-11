using UnityEngine;
using System.Collections;

public class ShipCollider : MonoBehaviour
{

	public GameObject source;
	public bool isTouched = false;
	public ParticleSystem particle;

			
	void Start(){
		isTouched = false;
	}

	void Update(){
		
	}

	void OnCollisionEnter (Collision col)
	{
		isTouched = true;

		if(col.gameObject.name == "Earth")
		{			
			particle.Play ();
			col.gameObject.GetComponent<MeshRenderer> ().enabled = false;





		}
	}



}