using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

[System.Serializable]
[XmlInclude(typeof(OneHandedMelee))]
[XmlInclude(typeof(TwoHandedMelee))]
[XmlInclude(typeof(Bow))]
[XmlInclude(typeof(Staff))]
[XmlInclude(typeof(Shield))]
[XmlInclude(typeof(HeadArmor))]
[XmlInclude(typeof(BodyArmor))]
[XmlInclude(typeof(HandArmor))]
[XmlInclude(typeof(LegArmor))]
public class Item{
	public string ID;
	public int value;
	public string inventoryName;
	public string description;
	public string icon;
	//public Sprite droppedSprite;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public enum ItemType
	{
		Weapon,
		Armor,
		Consumable
	};

	public ItemType itemType;
}
