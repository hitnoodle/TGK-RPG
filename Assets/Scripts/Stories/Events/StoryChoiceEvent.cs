using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace RPG.Stories
{
	[System.Serializable]
	public class StoryChoiceEvent : StoryEvent 
	{
		/// <summary>
		/// Story Text information.
		/// </summary>
		public StoryText CurrentText;

		public List<string> Choices;

		public StoryChoiceEvent() : base()
		{
			Type = StoryEventType.Choice;

			CurrentText = new StoryText();

			Choices = new List<string>();
			Choices.Add("");
			Choices.Add("");

			NextID.Clear();
			NextID.Add(0);
			NextID.Add(0);
		}

		public StoryChoiceEvent(StoryText text, List<string> choices) : base()
		{
			Type = StoryEventType.Choice;

			CurrentText = text;

			Choices = choices;

			int diff = Choices.Count - NextID.Count;
			for(int i=0;i<diff;i++) NextID.Add(0);
		}
	}
}