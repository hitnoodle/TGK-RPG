using System.Collections;

namespace RPG.Stories
{
	[System.Serializable]
	public class StoryTextEvent : StoryEvent  
	{
		/// <summary>
		/// Story Text information.
		/// </summary>
		public StoryText CurrentText;

		public StoryTextEvent() : base()
		{
			Type = StoryEventType.Text;

			CurrentText = new StoryText();
		}

		public StoryTextEvent(StoryText text) : base()
		{
			Type = StoryEventType.Text;

			CurrentText = text;
		}
	}
}