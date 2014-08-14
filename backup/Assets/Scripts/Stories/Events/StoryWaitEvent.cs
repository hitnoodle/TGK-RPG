using System.Collections;

namespace RPG.Stories
{
	[System.Serializable]
	public class StoryWaitEvent : StoryEvent 
	{
		/// <summary>
		/// What are we waiting for?
		/// </summary>
		public enum WaitMode
		{
			Seconds,
			Continue
		};
		public WaitMode Mode;

		/// <summary>
		/// How long to wait if in seconds.
		/// </summary>
		public float Seconds;

		public StoryWaitEvent() : base()
		{
			Type = StoryEventType.Wait;
			
			Mode = WaitMode.Continue;
		}

		public StoryWaitEvent(WaitMode mode) : base()
		{
			Type = StoryEventType.Wait;

			Mode = mode;
		}
	}
}