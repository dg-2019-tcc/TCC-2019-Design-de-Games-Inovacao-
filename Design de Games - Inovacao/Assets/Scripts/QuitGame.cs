using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public GameObject safetyQuestion;

    public void Quit()
    {
        safetyQuestion.SetActive(true);
    }

    public void NoQuit()
    {
        safetyQuestion.SetActive(false);
    }

    public void YesQuit()
    {
        Application.Quit();
    }
}
