using System.Collections;

namespace RPG.Stories
{
	[System.Serializable]
	public class StoryText
	{
		/// <summary>
		/// Name of the character currently speaking, if needed.
		/// </summary>
		public string Name;
		
		/// <summary>
		/// Texts of the story.
		/// </summary>
		public string Text;

		/// <summary>
		/// Does this text need advanced settings?
		/// </summary>
		public bool Advanced = false;

		/// <summary>
		/// Metadata contain the misc information, maybe using JSON format.
		/// Ex: { "mode":"narrative", "expression":"sad"}
		/// </summary>
		public string Metadata;
		
		/// <summary>
		/// Refer to voice or audio file of the text.
		/// </summary>
		public string Audio;
		
		/// <summary>
		/// Delay for the audio in seconds.
		/// </summary>
		public float AudioDelay;
	}
}