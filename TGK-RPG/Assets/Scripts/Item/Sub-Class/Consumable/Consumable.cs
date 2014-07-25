using UnityEngine;
using System.Collections;

public class Consumable : Item {

	public void consume(){
	
		consumeEffect();
	}

	public virtual void consumeEffect(){
	
	}
}
