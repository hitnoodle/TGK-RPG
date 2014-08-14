using UnityEngine;
using System.Collections;
using System.IO;

public static class ItemManager {

	public static Item LoadItem(string name){
		Item item = null;

		if (name != "")
		{
			string path = Application.persistentDataPath + "/" + name;
			
			if (File.Exists(path))
				item = (Item)XmlManager.LoadInstanceAsXml(name, typeof(Item));
		}

		return item;
	}

}
