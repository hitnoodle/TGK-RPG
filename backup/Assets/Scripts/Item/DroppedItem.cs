using UnityEngine;
using System.Collections;

public class DroppedItem : MonoBehaviour {

	public Item item;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void addToInventory(){
		GameObject.Find("FighterInventory").GetComponent<FighterInventory>().AddToInventory(item,1);
		Destroy(gameObject);
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag=="Player"){
			addToInventory();
		}
	}
}
