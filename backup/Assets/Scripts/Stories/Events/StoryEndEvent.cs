using System.Collections;

namespace RPG.Stories
{
	public class StoryEndEvent : StoryEvent  
	{
		public StoryEndEvent() : base()
		{
			Type = StoryEventType.End;
		}
	}
}