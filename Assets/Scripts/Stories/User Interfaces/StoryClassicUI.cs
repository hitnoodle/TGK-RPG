using UnityEngine;
using System.Collections;

using RPG.Stories;

public class StoryClassicUI : MonoBehaviour 
{
	public float ShowDelay = 0.2f;
	public int LineLength = 50;

	public SpriteRenderer Box;

	public TextMesh TextName;
	public TextMesh TextStory;

	private bool started = false;
	private PlayerControl playerControl;

	// Use this for initialization
	void Start () 
	{
		StoryTeller.OnStart += Show;
		StoryTeller.OnTextEvent += ShowCurrentText;
		StoryTeller.OnEnd += Hide;

		playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
	}

	IEnumerator ShowUIRoutine(float delay)
	{
		playerControl.BlockInput(delay);

		yield return new WaitForSeconds(delay);

		Box.gameObject.SetActive(true);
		
		TextName.gameObject.SetActive(true);
		TextStory.gameObject.SetActive(true);

		started = true;
	}

	void Show()
	{
		StartCoroutine(ShowUIRoutine(ShowDelay));
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
		TextName.text = storyText.Name;
		TextStory.text = ResolveTextSize(storyText.Text, LineLength);
	}
	
	void Hide()
	{
		started = false;

		Box.gameObject.SetActive(false);
		
		TextName.gameObject.SetActive(false);
		TextStory.gameObject.SetActive(false);
		
		playerControl.UnblockInput();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (started)
		{
			if (Input.GetKeyDown(KeyCode.Space)) StoryTeller.Continue();
		}
	}
}
