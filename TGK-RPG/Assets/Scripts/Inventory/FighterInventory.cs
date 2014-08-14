using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FighterInventory : Inventory {
	public List<Armor> armors;
	public List<Weapon> weapons;
	// Use this for initialization
	void Start () {
		if(armors == null)
			armors = new List<Armor>();
		if(weapons == null)
			weapons = new List<Weapon>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void AddEquipment (Equipment equipment) {
		if(equipment is Armor) {
			foreach(Armor armor in armors) {
				if(armor.GetType()==equipment.GetType()){
					int index = armors.IndexOf(armor);
					AddToInventory(armor,1);
					armors[index] = equipment as Armor;
					RemoveFromInventory(equipment,1);
					return;
				}
			}
			armors.Add(equipment as Armor);
		}else if(equipment is Weapon){
			if(equipment is TwoHanded){
				foreach(Weapon weapon in weapons){
					AddToInventory(weapon,1);
				}
				weapons.Clear();
				weapons.Add(equipment as Weapon);
			}else if(equipment is OneHanded){
				if(weapons.Count < 2){
					if(weapons.Count==1&&weapons[0] is TwoHanded){
						AddToInventory(weapons[0] as Weapon,1);
						weapons[0] = equipment as Weapon;
					}
					else
						weapons.Add(equipment as Weapon);
				}else{
					AddToInventory(weapons[0] as Weapon,1);
					weapons[0] = weapons[1];
					weapons[1] = equipment as Weapon;
				}
			}
		}
		RemoveFromInventory(equipment,1);
	}
	
	public void RemoveEquipment (Equipment equipment) {
		if(equipment is Armor){
			if(armors.Contains(equipment as Armor)){
				armors.Remove(equipment as Armor);
				AddToInventory(equipment,1);
			}
		}else if(equipment is Weapon){
			if(weapons.Contains(equipment as Weapon)){
				weapons.Remove(equipment as Weapon);
				AddToInventory(equipment,1);
			}
		}
	}
	
}
