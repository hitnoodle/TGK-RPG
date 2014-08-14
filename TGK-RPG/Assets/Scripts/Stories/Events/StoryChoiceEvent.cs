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

			NextID.Clear();
		}

		public StoryChoiceEvent(StoryText text, List<string> choices) : base()
		{
			Type = StoryEventType.Choice;

			CurrentText = text;

			Choices = choices;

			NextID.Clear();
			for(int i=0;i<Choices.Count;i++) NextID.Add(0);
		}
	}
}