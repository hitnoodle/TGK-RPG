using UnityEngine;
using System.Collections;

public class InteractiveObject : MonoBehaviour {

	private KeyCode interactKey = KeyCode.E;

	// Update is called once per frame
	void Update () {
		if(isInRange&&Input.GetKeyDown(interactKey))
			interact();
	}

	public virtual void interact(){}
	
	private bool isInRange = false;
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag=="Player"){
			isInRange=true;
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		if(other.tag=="Player"){
			isInRange=false;
		}
	}
}
