using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using RPG.Stories;

public class StoryDesigner : MonoBehaviour 
{
	/// <summary>
	/// Story ID to be loaded / created.
	/// </summary>
	public string StoryID;

	/// <summary>
	/// Is current on edit mode?
	/// </summary>
	public bool IsEditing = false;

	/// <summary>
	/// Story to be designed.
	/// </summary>
	public Story _Story;

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
		_Story = new Story();

		_Story.ID = StoryID;
		_Story.StoryEvents = new List<StoryEvent>();
		_Story.StoryEvents.Add(new StoryEndEvent());

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
				_Story = (Story)XmlManager.LoadInstanceAsXml(id, typeof(Story));
			}

			IsEditing = true;
		}
	}
	
	public void Clear()
	{
		_Story.ID = "";
		_Story.StoryEvents = new List<StoryEvent>();
		_Story.StoryEvents.Add(new StoryEndEvent());
	}

	public void Save()
	{
		if (_Story.ID != "")
			XmlManager.SaveInstanceAsXml(_Story.ID, typeof(Story), _Story);
	}

	public void Done()
	{
		//Save();
		//Clear();

		IsEditing = false;
	}
}
