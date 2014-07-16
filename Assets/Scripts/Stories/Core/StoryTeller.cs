using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

		public delegate void onChoiceEvent(StoryText storyText, List<string> choices);
		public static onChoiceEvent OnChoiceEvent;

		public delegate void onWaitEventStart(bool waitForContinue);
		public static onWaitEventStart OnWaitEventStart;

		public delegate void onWaitEventEnd();
		public static onWaitEventEnd OnWaitEventEnd;

		public delegate void onMessageEvent(string message, string metadata);
		public static onMessageEvent OnMessageEvent;

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

		IEnumerator Wait(float seconds)
		{
			yield return new WaitForSeconds(seconds);

			if (OnWaitEventEnd != null)
				OnWaitEventEnd();

			Continue();
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
				// Call wait complete if our last event are wait
				if (_Instance.currentEvent.Type == StoryEvent.StoryEventType.Wait)
				{
					_Instance.StopAllCoroutines();

					if (OnWaitEventEnd != null)
						OnWaitEventEnd();
				}

				// Get next event
				_Instance.currentId = _Instance.currentEvent.NextID[choice];
				_Instance.currentEvent = _Instance._Story.eventAtID(_Instance.currentId); // Maybe null
			}

			// Call delegate according to what kind of event right now
			if (_Instance.currentEvent != null)
			{
				StoryEvent.StoryEventType evType = 	_Instance.currentEvent.Type;

				if (evType == StoryEvent.StoryEventType.Text)
				{
					StoryTextEvent sev = (StoryTextEvent) _Instance.currentEvent;

					if (OnTextEvent != null)
						OnTextEvent(sev.CurrentText);
				}
				else if (evType == StoryEvent.StoryEventType.Choice)
				{
					StoryChoiceEvent sev = (StoryChoiceEvent) _Instance.currentEvent;
					
					if (OnChoiceEvent != null)
						OnChoiceEvent(sev.CurrentText, sev.Choices);
				}
				else if (evType == StoryEvent.StoryEventType.Wait)
				{
					StoryWaitEvent sev = (StoryWaitEvent) _Instance.currentEvent;

					if (sev.Mode == StoryWaitEvent.WaitMode.Seconds)
						_Instance.StartCoroutine(_Instance.Wait(sev.Seconds));

					if (OnWaitEventStart != null)
					{
						bool waitForContinue = (sev.Mode == StoryWaitEvent.WaitMode.Continue);
						OnWaitEventStart(waitForContinue);
					}
				}
				else if (evType == StoryEvent.StoryEventType.Message)
				{
					StoryMessageEvent sev = (StoryMessageEvent) _Instance.currentEvent;
					
					if (OnMessageEvent != null)
						OnMessageEvent(sev.Message, sev.Metadata);
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