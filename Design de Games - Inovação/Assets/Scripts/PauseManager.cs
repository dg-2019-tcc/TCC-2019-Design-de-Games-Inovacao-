using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    public GameObject pausebuttons;

    public void Pause()
    {
        pausebuttons.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void VoltaMenu()
    {
        SceneManager.LoadScene("DelayStartMenuDemo");
    }

    public void VoltaJogo()
    {
        pausebuttons.SetActive(false);
    }
}
