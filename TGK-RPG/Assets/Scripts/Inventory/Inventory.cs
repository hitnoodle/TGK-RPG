using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public List<Item> items;
	public List<int> amounts;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void AddToInventory (Item item, int amount) {
		if(items.Contains(item)){
			amounts[items.IndexOf(item)]+=amount;
		}else{
			items.Add(item);
			amounts.Add(amount);
		}
		Debug.Log(item.ID+" has been added to your inventory");
	}

	public void RemoveFromInventory (Item item, int amount) {
		if(items.Contains(item)){
			amounts[items.IndexOf(item)]-=amount;
			if(amounts[items.IndexOf(item)]<=0){
				amounts.RemoveAt(items.IndexOf(item));
				items.Remove(item);
			}
		}
		Debug.Log(item.ID+" has been removed from your inventory");
	}
	
}
