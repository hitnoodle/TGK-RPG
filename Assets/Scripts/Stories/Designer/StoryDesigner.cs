using UnityEngine;
using System.Collections;

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
		_Story.ID = StoryID;
		_Story.StoryEvents = new StoryEvent[1];
		_Story.StoryEvents[0] = new StoryEndEvent();

		IsEditing = true;
	}
	
	public void Load(string id)
	{
		if (id != "")
			Create(id);
	}
	
	public void Clear()
	{
		_Story.ID = "";
		_Story.StoryEvents = new StoryEvent[1];
		_Story.StoryEvents[0] = new StoryEndEvent();
	}

	public void Save()
	{
		
	}

	public void Done()
	{
		Save();
		Clear();

		IsEditing = false;
	}
}
