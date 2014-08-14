using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using RPG.Stories;

public class ShopDesigner : MonoBehaviour {
	/// <summary>
	/// Story ID to be loaded / created.
	/// </summary>
	public string shopId;
	
	/// <summary>
	/// Is current on edit mode?
	/// </summary>
	public bool IsEditing = false;
	
	/// <summary>
	/// Story to be designed.
	/// </summary>
	public Shop OShop;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void Create(string id)
	{
		OShop = new Shop();
		
		OShop.shopId = shopId;
		OShop.sellValue = 0;
		OShop.listSellItemId = new List<string>();
		OShop.listSellItemId.Add("");
		
		IsEditing = true;
	}
	
	public void Load(string id)
	{
		if (id != "")
		{
			//See path
			string path = Application.persistentDataPath + "/" + id;
			
			//Create first if haven't
			if (!File.Exists(path))
			{
				Create(id);
				Save();
			} 
			else
			{
				//Load
				OShop = (Shop)XmlManager.LoadInstanceAsXml(id, typeof(Shop));
			}
			
			IsEditing = true;
		}
	}
	
	public void Clear()
	{
		OShop.shopId = "";
		OShop.sellValue = 0;
		OShop.listSellItemId = new List<string>();
		OShop.listSellItemId.Add("");
	}
	
	public void Save()
	{
		if (OShop.shopId != "")
			XmlManager.SaveInstanceAsXml(OShop.shopId, typeof(Shop), OShop);
	}
	
	public void Done()
	{
		//Save();
		//Clear();
		
		IsEditing = false;
	}

}
