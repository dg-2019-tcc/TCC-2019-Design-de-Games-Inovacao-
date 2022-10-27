using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLanguage : MonoBehaviour
{
    public ButtonVisual[] allButtons;

    private void Start()
    {
        allButtons = Resources.FindObjectsOfTypeAll(typeof(ButtonVisual)) as ButtonVisual[];
    }

    public void ChangeLingua()
    {
        if(GameManager.languageIndex == 0)
        {
            GameManager.languageIndex = 1;
        }
        else
        {
            GameManager.languageIndex = 0;
        }

        for(int i = 0; i < allButtons.Length; i++)
        {
            if (allButtons[i].isActiveAndEnabled)
            {
                allButtons[i].Change();
            }
        }
    }
}
