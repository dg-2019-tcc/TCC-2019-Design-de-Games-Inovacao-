using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonVisual : MonoBehaviour
{
	public TranslateVariable language;
	public ScriptableButtons scriptableButtons;
	private Image image;
	public TMP_Text text;


    void Start()
    {
		image = GetComponent<Image>();
		image.sprite = scriptableButtons.image[language.languageIndex];
		text.text = scriptableButtons.text[language.languageIndex];
    }
	

}
