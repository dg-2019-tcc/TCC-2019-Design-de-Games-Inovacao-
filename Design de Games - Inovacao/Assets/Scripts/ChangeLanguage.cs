using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLanguage : MonoBehaviour
{
    public ButtonVisual[] allButtons;

    private void Start()
    {
        allButtons = FindObjectsOfTypeAll(typeof(ButtonVisual)) as ButtonVisual[];
    }

    public void ChangeLingua()
    {
        if(GameManager.languageIndex == 0)
        {
            GameManager.languageIndex = 1;
            Debug.Log("Esta em Inglês");
        }
        else
        {
            GameManager.languageIndex = 0;
            Debug.Log("Esta em Português");
        }

        for(int i = 0; i < allButtons.Length; i++)
        {
            allButtons[i].Change();
        }
    }
}
