using System.Collections;
using System.Collections.Generic;

namespace RPG.Stories
{
	[System.Serializable]
	public class Story 
	{
		/// <summary>
		/// ID of this story.
		/// </summary>
		public string ID;

		
		//TODO: [RPG.Stories] Change events to Dictionary
		//public Dictionary<int, StoryEvent> StoryEvents;

		/// <summary>
		/// All the events information of this story.
		/// </summary>
		public List<StoryEvent> StoryEvents;

		/// <summary>
		/// Return story events at ID 
		/// </summary>
		public StoryEvent eventAtID(int id)
		{
			foreach(StoryEvent ev in StoryEvents)
			{
				if (ev.ID == id) return ev;
			}

			return null;
		}
	}
}
