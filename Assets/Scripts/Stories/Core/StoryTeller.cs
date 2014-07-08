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

		void Awake()
		{
			_Instance = this;
		}

		// Use this for initialization
		void Start() 
		{
		
		}
		
		// Update is called once per frame
		void Update() 
		{
		
		}

		public static void Start(string story)
		{
			if (_Instance == null) return;
		}

		public static void Continue()
		{
			if (_Instance == null) return;
		}

		public static void Continue(int choice)
		{
			if (_Instance == null) return;
		}

		public static void End()
		{
			if (_Instance == null) return;
		}
	}
}