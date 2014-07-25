using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreasureChest : InteractiveObject {

	public List<string> items;
	public bool isOpened;
	public Sprite openedChest;

	// Use this for initialization
	void Start () {
		isOpened = false;
	}

	public override void interact(){
		if(!isOpened){
			FighterInventory fi = GameObject.Find("FighterInventory").GetComponent<FighterInventory>();
			foreach(string name in items)
				fi.AddToInventory(ItemManager.LoadItem(name),1);
			isOpened = true;
			
			GetComponent<SpriteRenderer>().sprite = openedChest;
		}
	}
}
