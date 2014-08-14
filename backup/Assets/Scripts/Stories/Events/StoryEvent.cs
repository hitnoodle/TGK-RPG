using System.Collections;
using System.Collections.Generic;

using System.Xml;
using System.Xml.Serialization;

using UnityEngine;

namespace RPG.Stories
{
	// Hack, not abstract yet.
	[System.Serializable]
	[XmlInclude(typeof(StoryTextEvent))]
	[XmlInclude(typeof(StoryChoiceEvent))]
	[XmlInclude(typeof(StoryWaitEvent))]
	[XmlInclude(typeof(StoryMessageEvent))]
	[XmlInclude(typeof(StoryEndEvent))]
	public class StoryEvent 
	{
		/// <summary>
		/// ID of the event in the story.
		/// </summary>
		public int ID;

		/// <summary>
		/// Next ID after this event.
		/// Note: End type doesn't have next event, so this will be null on that.
		/// </summary>
		public List<int> NextID;

		/// <summary>
		/// Type of event that can happen.
		/// </summary>
		public enum StoryEventType
		{
			Text,
			Choice,
			Wait,
			Message,
			End
		};
		public StoryEventType Type;

		public StoryEvent()
		{
			NextID = new List<int>();
		}
	}
}