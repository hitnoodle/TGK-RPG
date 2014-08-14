using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FighterInventory : Inventory {
	public List<Equipment> equipped;

	// Use this for initialization
	void Start () {
		//foreach(Equipment equipment in test){
		//	AddEquipment(equipment);
		//}

	}

	// Update is called once per frame
	void Update () {

	}

	public List<Equipment> AddEquipment (Equipment equipment) {
		List<Equipment> toBeRemoved = new List<Equipment>();
		for(int i=0;i<equipped.Count;i++){
			if(equipped[i].GetType() == equipment.GetType()){
				AddToInventory(equipped[i],1);
				equipped[i] = equipment;
				toBeRemoved.Add(equipped[i]);
				return null;
			}
			if(equipment is TwoHanded){
				if(equipped[i] is OneHanded)
					toBeRemoved.Add(equipped[i]);
			}
			if(equipment is OneHanded){
				if(equipped[i] is TwoHanded)
					toBeRemoved.Add(equipped[i]);
			}
		}
		foreach(Equipment equip in toBeRemoved){
			AddToInventory(equip,1);
			equipped.Remove(equip);
		}
		equipped.Add(equipment);
		return null;
	}

	public void RemoveEquipment (Equipment equipment) {
		if(equipped.Contains(equipment)){
			AddToInventory(equipment,1);
			equipped.Remove(equipment);
		}
	}
}
