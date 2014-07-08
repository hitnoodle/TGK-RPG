﻿using UnityEngine;
using UnityEditor;
using System.Collections;

using RPG.Stories;

// TODO: This sucks code-wise and experience-wise, change this to custom window editor later.
[CustomEditor(typeof(StoryDesigner), true)]
public class StoryDesignerEditor : Editor 
{
	public void OnEnable()
	{
		if(serializedObject == null)
			return;
	}

	void DrawLoadLayout(StoryDesigner designer)
	{
		SerializedProperty storyID = serializedObject.FindProperty("StoryID");
		storyID.stringValue = EditorGUILayout.TextField ("Story ID", storyID.stringValue);
		
		if (GUILayout.Button("Load / Create"))
			designer.Load(designer.StoryID);
	}
	
	void DrawSaveLayout(StoryDesigner designer)
	{
		EditorGUILayout.BeginHorizontal();
		
		if (GUILayout.Button("Clear"))
			designer.Clear();
		
		if (GUILayout.Button("Save"))
			designer.Save();
		
		EditorGUILayout.EndHorizontal();
		
		if (GUILayout.Button("Done"))
			designer.Done();
	}

	void DrawStoryLayout(Story story)
	{
		SerializedProperty storyProperty = serializedObject.FindProperty("_Story");

		SerializedProperty storyID = storyProperty.FindPropertyRelative("ID");
		storyID.stringValue = EditorGUILayout.TextField ("ID", storyID.stringValue);

		SerializedProperty storyEventArray = storyProperty.FindPropertyRelative("StoryEvents").FindPropertyRelative("Array");

		SerializedProperty storyEventSize = storyEventArray.FindPropertyRelative("size");
		storyEventSize.intValue = EditorGUILayout.IntField ("Event Length", storyEventSize.intValue);

		if (storyEventSize.intValue == 0)
		{
			storyEventSize.intValue = 1;
		}

		EditorGUILayout.Space();

		int length = storyEventArray.arraySize;
		for(int i=0;i<length;i++) 
		{
			SerializedProperty ev = storyEventArray.FindPropertyRelative ("data[" + i + "]");

			SerializedProperty evID = ev.FindPropertyRelative("ID");
			evID.intValue = EditorGUILayout.IntField ("ID", evID.intValue);

			SerializedProperty evType = ev.FindPropertyRelative("Type");
			EditorGUILayout.PropertyField(evType);

			int enum_index = evType.enumValueIndex;
			
			StoryEvent storyEvent = null;
			if (i >= 0 && i < story.StoryEvents.Length) storyEvent = story.StoryEvents[i];
			
			if (storyEvent != null)
			{
				if (enum_index == 0) // Text
				{
					StoryTextEvent storyTextEvent = storyEvent as StoryTextEvent;
					
					if (storyTextEvent != null) // Casted, update using editor
					{
						storyTextEvent.CurrentText.Name = EditorGUILayout.TextField ("Name", storyTextEvent.CurrentText.Name);
						storyTextEvent.CurrentText.Text = EditorGUILayout.TextField ("Text", storyTextEvent.CurrentText.Text);
						storyTextEvent.CurrentText.Advanced = EditorGUILayout.Toggle ("Show Advanced Options", storyTextEvent.CurrentText.Advanced);
						
						if (storyTextEvent.CurrentText.Advanced)
						{
							storyTextEvent.CurrentText.Metadata = EditorGUILayout.TextField ("Metadata", storyTextEvent.CurrentText.Metadata);
							storyTextEvent.CurrentText.Audio = EditorGUILayout.TextField ("Audio", storyTextEvent.CurrentText.Audio);
							storyTextEvent.CurrentText.AudioDelay = EditorGUILayout.FloatField ("Audio Delay", storyTextEvent.CurrentText.AudioDelay);
						}
					}
					else // First time, create new child
					{
						storyTextEvent = new StoryTextEvent();
						story.StoryEvents[i] = storyTextEvent;
					}
				}
				else if (enum_index == 1) // Choice
				{
					StoryChoiceEvent storyChoiceEvent = storyEvent as StoryChoiceEvent;
					
					if (storyChoiceEvent != null) // Casted, update using editor
					{
						storyChoiceEvent.CurrentText.Name = EditorGUILayout.TextField ("Name", storyChoiceEvent.CurrentText.Name);
						storyChoiceEvent.CurrentText.Text = EditorGUILayout.TextField ("Text", storyChoiceEvent.CurrentText.Text);
						storyChoiceEvent.CurrentText.Advanced = EditorGUILayout.Toggle ("Show Advanced Options", storyChoiceEvent.CurrentText.Advanced);

						if (storyChoiceEvent.CurrentText.Advanced)
						{
							storyChoiceEvent.CurrentText.Metadata = EditorGUILayout.TextField ("Metadata", storyChoiceEvent.CurrentText.Metadata);
							storyChoiceEvent.CurrentText.Audio = EditorGUILayout.TextField ("Audio", storyChoiceEvent.CurrentText.Audio);
							storyChoiceEvent.CurrentText.AudioDelay = EditorGUILayout.FloatField ("Audio Delay", storyChoiceEvent.CurrentText.AudioDelay);
						}

						EditorGUILayout.Space();

						int choiceNum = storyChoiceEvent.Choices.Count;
						for(int j=0;j<choiceNum;j++)
						{
							EditorGUILayout.BeginHorizontal();

							storyChoiceEvent.Choices[j] = EditorGUILayout.TextField ("Choice " + j, storyChoiceEvent.Choices[j]);

							if (EditorGUILayout.Toggle(false, GUILayout.Width(15)))
							{
								if (choiceNum > 2)
								{
									storyChoiceEvent.Choices.RemoveAt(j);
									storyChoiceEvent.NextID.RemoveAt(j);
									break;
								}
							}

							EditorGUILayout.EndHorizontal();

							if (j > -1 && j < storyEvent.NextID.Count)
								storyEvent.NextID[j] = EditorGUILayout.IntField ("To ID", storyEvent.NextID[j]);
						}

						if (EditorGUILayout.Toggle("Add More Choice", false))
						{
							storyChoiceEvent.Choices.Add("");
							storyChoiceEvent.NextID.Add(0);
						}
					}
					else // First time, create new child
					{
						storyChoiceEvent = new StoryChoiceEvent();
						story.StoryEvents[i] = storyChoiceEvent;
					}
				}
				else if (enum_index == 2) // Wait
				{
					StoryWaitEvent storyWaitEvent = storyEvent as StoryWaitEvent;
					
					if (storyWaitEvent != null) // Casted, update using editor
					{
						storyWaitEvent.Mode = (StoryWaitEvent.WaitMode) EditorGUILayout.EnumPopup("Wait Mode", storyWaitEvent.Mode);
						
						if (storyWaitEvent.Mode == StoryWaitEvent.WaitMode.Seconds)
						{
							storyWaitEvent.Seconds = EditorGUILayout.FloatField ("Time", storyWaitEvent.Seconds);
						}
					}
					else // First time, create new child
					{
						storyWaitEvent = new StoryWaitEvent(StoryWaitEvent.WaitMode.Continue);
						story.StoryEvents[i] = storyWaitEvent;
					}
				}
				else if (enum_index == 3) // Message
				{
					StoryMessageEvent storyMessageEvent = storyEvent as StoryMessageEvent;
					
					if (storyMessageEvent != null) // Casted, update using editor
					{
						storyMessageEvent.Message = EditorGUILayout.TextField ("Message", storyMessageEvent.Message);
						storyMessageEvent.Metadata = EditorGUILayout.TextField ("Metadata", storyMessageEvent.Metadata);
					}
					else // First time, create new child
					{
						storyMessageEvent = new StoryMessageEvent("","");
						story.StoryEvents[i] = storyMessageEvent;
					}
				}
				else if (enum_index == 4) // End
				{
					StoryEndEvent storyEndEvent = storyEvent as StoryEndEvent;
					
					if (storyEndEvent != null) // Casted, update using editor
					{
						
					}
					else // First time, create new child
					{
						storyEndEvent = new StoryEndEvent();
						story.StoryEvents[i] = storyEndEvent;
					}
				}

				// Choice and End event is special
				if (enum_index != 1 && enum_index != 4)
					storyEvent.NextID[0] = EditorGUILayout.IntField ("To ID", storyEvent.NextID[0]);
			}

			EditorGUILayout.Space();
		}
	}

	public override void OnInspectorGUI ()
	{	
		// Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
		serializedObject.Update ();

		StoryDesigner script = (StoryDesigner) target;

		DrawLoadLayout(script);

		if (script.IsEditing)
		{
			EditorGUILayout.LabelField("=========================================");

			DrawStoryLayout(script._Story);

			DrawSaveLayout(script);
			
			EditorGUILayout.LabelField("=========================================");
		}
		
		// Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
		serializedObject.ApplyModifiedProperties ();
	}
}