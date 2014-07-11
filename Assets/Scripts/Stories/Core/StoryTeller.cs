using UnityEngine;
using System.Collections;

namespace RPG.Stories
{
	/// <summary>
	/// Story teller module. Based on Dialoguer, INheritage, and Pale Blue Mockup.
	/// </summary>
	public class StoryTeller : MonoBehaviour 
	{
		private static StoryTeller _Instance;

		public delegate void onStart();
		public static onStart OnStart;

		public delegate void onTextEvent(StoryText storyText);
		public static onTextEvent OnTextEvent;

		public delegate void onEnd();
		public static onEnd OnEnd;

		protected Story _Story;

		protected int currentId;
		protected StoryEvent currentEvent;

		void Awake()
		{
			_Instance = this;
		}

		// Use this for initialization
		void Start() 
		{
			currentId = -1;
		}
		
		// Update is called once per frame
		void Update() 
		{
		
		}

		void LoadStory(string story)
		{
			_Story = (Story)XmlManager.LoadInstanceAsXml(story, typeof(Story));

			if (_Story == null)
				Debug.LogError("[Story Teller] No story at file " + story);
		}

		public static void Start(string story)
		{
			if (_Instance == null) return;

			_Instance.LoadStory(story);
			_Instance.currentId = -1;

			if (OnStart != null)
				OnStart();

			Continue();
		}

		public static void Continue()
		{
			Continue(0);
		}

		public static void Continue(int choice)
		{
			if (_Instance == null) return;
			if (_Instance._Story == null) return;

			// Assume first node is always the first
			if (_Instance.currentId == -1)
			{
				// Get first event 
				_Instance.currentEvent = _Instance._Story.StoryEvents[0];
				_Instance.currentId = _Instance.currentEvent.ID;
			}
			else
			{
				// Get next event
				_Instance.currentId = _Instance.currentEvent.NextID[choice];
				_Instance.currentEvent = _Instance._Story.eventAtID(_Instance.currentId); // Maybe null
			}

			if (_Instance.currentEvent != null)
			{
				StoryEvent.StoryEventType evType = 	_Instance.currentEvent.Type;

				if (evType == StoryEvent.StoryEventType.Text)
				{
					StoryTextEvent sev = (StoryTextEvent) _Instance.currentEvent;

					if (OnTextEvent != null)
						OnTextEvent(sev.CurrentText);
				}
				else if (evType == StoryEvent.StoryEventType.End)
				{
					End();
				}
			}
			else
			{
				Debug.LogError("[Story Teller] There is no event at id " + _Instance.currentId);
			}
		}

		public static void End()
		{
			if (_Instance == null) return;

			_Instance._Story = null;

			if (OnEnd != null)
				OnEnd();
		}
	}
}