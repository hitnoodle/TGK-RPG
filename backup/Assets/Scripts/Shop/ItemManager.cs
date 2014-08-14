using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ItemManager {
	public Item item;
	public int amount;

	public ItemManager(Item item, int amount){
		this.item = item;
		this.amount = amount;
	}

}
