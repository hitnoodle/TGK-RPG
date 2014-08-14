using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using RPG.Stories;

public class StoryClassicUI : MonoBehaviour 
{
	public float ShowDelay = 0.2f;
	public int LineLength = 50;

	public SpriteRenderer Box;

	public TextMesh TextName;
	public TextMesh TextStory;

	public SpriteRenderer ChoiceBox;
	public TextMesh ChoiceText;

	// Hail flag
	private bool started = false;
	private bool waiting = false;

	private bool displayChoice = false;

	private List<string> currentChoices;
	private int choiceSelected;
	private Vector3 defaultBoxScale;

	private PlayerControl playerControl;

	// Use this for initialization
	void Start () 
	{
		StoryTeller.OnStart += Show;
		StoryTeller.OnTextEvent += ShowCurrentText;
		StoryTeller.OnChoiceEvent += ShowCurrentChoice;
		StoryTeller.OnEnd += Hide;
		StoryTeller.OnWaitEventStart += HideWait;
		StoryTeller.OnWaitEventEnd += ShowWait;

		choiceSelected = 0;
		defaultBoxScale = transform.localScale;

		playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
	}

	void Show()
	{
		StartCoroutine(ShowUIRoutine(ShowDelay));
	}

	IEnumerator ShowUIRoutine(float delay)
	{
		playerControl.BlockInput(delay);
		
		yield return new WaitForSeconds(delay);
		
		ShowUI();
		started = true;
	}

	void ShowWait()
	{
		ShowUI();
		waiting = false;
	}

	void ShowUI()
	{
		Box.gameObject.SetActive(true);
		
		TextName.gameObject.SetActive(true);
		TextStory.gameObject.SetActive(true);
	}
	
	void ShowChoiceUI()
	{
		displayChoice = true;
		
		ChoiceBox.gameObject.SetActive(true);
		ChoiceText.gameObject.SetActive(true);
	}

	// Wrap text by line height
	private string ResolveTextSize(string input, int lineLength)
	{
		// Split string by char " "    
		string[] words = input.Split(" "[0]);
		
		// Prepare result
		string result = "";
		
		// Temp line string
		string line = "";
		
		// for each all words     
		foreach(string s in words){
			// Append current word into line
			string temp = line + " " + s;
			
			// If line length is bigger than lineLength
			if(temp.Length > lineLength){
				
				// Append current line into result
				result += line + "\n";
				// Remain word append into new line
				line = s;
			}
			// Append current word into current line
			else 
			{
				line = temp;
			}
		}
		
		// Append last line into result   
		result += line;
		
		// Remove first " " char
		return result.Substring(1,result.Length-1);
	}

	void ShowCurrentText(StoryText storyText)
	{
		HideChoiceUI();

		TextName.text = storyText.Name;
		TextStory.text = ResolveTextSize(storyText.Text, LineLength);
	}

	void ShowCurrentChoice(StoryText storyText, List<string> choices)
	{
		TextName.text = storyText.Name;
		TextStory.text = ResolveTextSize(storyText.Text, LineLength);

		ShowChoiceUI();

		currentChoices = choices;

		string choiceResolved = ResolveChoiceText(currentChoices, choiceSelected);
		ChoiceText.text = choiceResolved;

		ResolveChoiceBox(choices);
	}

	string ResolveChoiceText(List<string> choices, int choice)
	{
		string choices_string = "";
		for(int i=0;i<choices.Count;i++)
		{
			string s = "";
			if (i == choice) s += "> ";

			s += choices[i];
			choices_string += s;

			if (i != choices.Count - 1)
				choices_string += "\n";
		}

		return choices_string;
	}

	void ResolveChoiceBox(List<string> choiceText)
	{
		float height = (float)(choiceText.Count - 1) * 0.75f;

		int stringWidth = 4;
		foreach(string s in choiceText)
			stringWidth = Mathf.Max(stringWidth, s.Length + 2);

		ChoiceBox.transform.localScale = new Vector3(defaultBoxScale.x * ((float)stringWidth / 3.5f), defaultBoxScale.y + height, defaultBoxScale.z);
	}
	
	void Hide()
	{
		started = false;

		HideUI();
		
		playerControl.UnblockInput();
	}

	void HideWait(bool waitForContinue)
	{
		waiting = true;

		HideUI();
		HideChoiceUI();

		if (waitForContinue)
			StartCoroutine(TestWaitContinue());
	}

	void HideUI()
	{
		Box.gameObject.SetActive(false);
		
		TextName.gameObject.SetActive(false);
		TextStory.gameObject.SetActive(false);
	}

	void HideChoiceUI()
	{
		if (displayChoice)
		{
			choiceSelected = 0;
			currentChoices = null;

			ChoiceBox.gameObject.SetActive(false);
			ChoiceText.gameObject.SetActive(false);

			displayChoice = false;
		}
	}

	IEnumerator TestWaitContinue()
	{
		yield return new WaitForSeconds(2);

		StoryTeller.Continue();
	}
	
	// Update is called once per frame
	void Update() 
	{
		if (started && !waiting)
		{
			int choice = 0;

			if (displayChoice)
			{
				if (Input.GetKeyDown(KeyCode.UpArrow))
					choiceSelected--;
				else if (Input.GetKeyDown(KeyCode.DownArrow))
					choiceSelected++;

				choiceSelected = Mathf.Clamp(choiceSelected, 0, currentChoices.Count - 1);
				string choiceResolved = ResolveChoiceText(currentChoices, choiceSelected);
				ChoiceText.text = choiceResolved;

				choice = choiceSelected;
			}

			if (Input.GetKeyDown(KeyCode.Space)) StoryTeller.Continue(choice);
		}
	}
}
