using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Variables/Translation")]
public class TranslateVariable : ScriptableObject
{
	public string language;
	public int languageIndex;


	public void Update(string languageInput)
	{
		language = languageInput;
		switch (language)
		{
			case "Português":
				languageIndex = 0;
				break;
			case "English":
				languageIndex = 1;
				break;
			
			default:
				UnifiySpelling(language);
				break;
		}
	}

	public void UnifiySpelling(string word)
	{
		switch (word)
		{
			case "Portugues":
			case "portugues":
			case "português":
			case "Portuguese":
			case "portuguese":
				Update("Português");
				break;
			case "Inglês":
			case "inglês":
			case "Ingles":
			case "ingles":
			case "english":
				Update("English");
				break;

			default:
				Update("Português");
				break;
		}
	}
}
