using UnityEngine;
using System.Collections;

[System.Serializable]
public class SellItem {
	public string itemId;
	public int amount;

	public SellItem(){
	
	}

	public SellItem(string itemId, int amount){
		this.itemId = itemId;
		this.amount = amount;
	}
}
