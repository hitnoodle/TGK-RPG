using UnityEngine;
using System.Collections;

public class DroppedItem : InteractiveObject {

	public string itemName;
	public SpriteManager sm;
	private Item item;

	// Use this for initialization
	void Start () {
		if(itemName != ""){
			item = ItemManager.LoadItem(itemName);
			gameObject.GetComponent<SpriteRenderer>().sprite = sm.getDrop(itemName);
		}
		else
			item = null;
	}

	public override void interact(){
		GameObject.Find("FighterInventory").GetComponent<FighterInventory>().AddToInventory(item,1);
		Destroy(gameObject);
	}
}
