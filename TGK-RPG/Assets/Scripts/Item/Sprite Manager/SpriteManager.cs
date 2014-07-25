using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteManager : MonoBehaviour {

	public List<string> itemName;
	public List<SerializableSprite> itemIcon;
	public List<SerializableSprite> itemDropTexture;
	public List<SerializableSprite> itemEquipTexture;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Add(string name){
		itemName.Add(name);
		itemIcon.Add(null);
		itemDropTexture.Add(null);
		itemEquipTexture.Add(null);
	}

	public void Delete(string name){
		if(itemName.Contains(name)){
			int index = itemName.IndexOf(name);
			itemName.RemoveAt(index);
			itemIcon.RemoveAt(index);
			itemDropTexture.RemoveAt(index);
			itemEquipTexture.RemoveAt(index);
		}
	}

	public Sprite getIcon(string name){
		Sprite sprite = null;

		if(itemName.Contains(name))
			return itemIcon[itemName.IndexOf(name)].sprite;

		return sprite;
	}

	public Sprite getDrop(string name){
		Sprite sprite = null;
		
		if(itemName.Contains(name))
			return itemDropTexture[itemName.IndexOf(name)].sprite;
		
		return sprite;
	}

	public Sprite getEquip(string name){
		Sprite sprite = null;
		
		if(itemName.Contains(name))
			return itemEquipTexture[itemName.IndexOf(name)].sprite;
		
		return sprite;
	}
}
