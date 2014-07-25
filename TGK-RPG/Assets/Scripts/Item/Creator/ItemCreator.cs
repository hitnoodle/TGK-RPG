using UnityEngine;
using System.Collections;
using System.IO;

public class ItemCreator : MonoBehaviour {

	public string itemID;
	public Item _Item;
	public bool isEditing;
	public bool isCreating;
	public bool isNew;

	// Use this for initialization
	void Start () {
		isEditing = false;
		isCreating = false;
		isNew = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Create(string id)
	{
		_Item = new Item();
		_Item.ID = id;
		
		isCreating = true;
	}
	
	public void Load(string id)
	{
		if (id != "")
		{
			isEditing = false;
			isCreating = false;
			if(checkFileExistence(id)){
				_Item = (Item)XmlManager.LoadInstanceAsXml(id, typeof(Item));
				isEditing = true;
				isNew = false;
			}
			else{
				Create(id);
				isNew = true;
			}
		}
	}

	bool checkFileExistence(string id){
		string path = Application.persistentDataPath + "/" + id;

		if (!File.Exists(path))
			return false;
		return true;
	}

	public void Save()
	{
		if (_Item.ID != ""){
			XmlManager.SaveInstanceAsXml(_Item.ID, typeof(Item), _Item);
		}
	}

	public void Delete(SpriteManager sm){
		if (_Item.ID != ""){
			string path = Application.persistentDataPath + "/" + _Item.ID;
			FileInfo t = new FileInfo(path); 
			
			if(t.Exists){ 
				t.Delete();
				sm.Delete(_Item.ID);
			}

			isEditing = false;
		}
	}

	public void AddSpriteManager(SpriteManager sm){
		sm.Add(_Item.ID);
	}
	
	public void Done()
	{
		//Save();
		//Clear();

		isCreating = false;
		isEditing = false;
	}
}
