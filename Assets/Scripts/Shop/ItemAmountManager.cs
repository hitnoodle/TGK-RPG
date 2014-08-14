using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ItemAmountManager {
	public Item item;
	public int amount;

	public ItemAmountManager(Item item, int amount){
		this.item = item;
		this.amount = amount;
	}

}
