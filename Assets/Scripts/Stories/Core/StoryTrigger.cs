using UnityEngine;
using System.Collections;

namespace RPG.Stories
{
	public class StoryTrigger : WorldTrigger 
	{
		/// <summary>
		/// Story to be triggered. Have to already created.
		/// </summary>
		public string Story;

		/// <summary>
		/// Always trigger when activate/enter or just one time.
		/// </summary>
		public bool AlwaysTrigger = false;

		// Use this for initialization
		void Start () 
		{
			OnTrigger += CallStoryTeller;
		}
		
		void CallStoryTeller(GameObject objectOnTrigger)
		{
			StoryTeller.Start(Story);

			// Disable if just once
			if (!AlwaysTrigger)
			{
				OnTrigger -= CallStoryTeller;
				this.enabled = false;
			}
		}
	}
}
