using System.Collections;

namespace RPG.Stories
{
	[System.Serializable]
	public class StoryMessageEvent : StoryEvent  
	{
		/// <summary>
		/// What kind of message to send.
		/// </summary>
		public string Message;

		/// <summary>
		/// Message metadata to be informed.
		/// </summary>
		public string Metadata;

		public StoryMessageEvent() : base()
		{
			Type = StoryEventType.Message;
			
			Message = "";
			Metadata = "";
		}

		public StoryMessageEvent(string message, string metadata) : base()
		{
			Type = StoryEventType.Message;

			Message = message;
			Metadata = metadata;
		}
	}
}