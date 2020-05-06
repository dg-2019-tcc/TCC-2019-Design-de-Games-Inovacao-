using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortaTutorial : MonoBehaviour
{
    public Joystick joy;

    private float joyGambiarra;
    public bool abriPorta;

    // Start is called before the first frame update
    void Start()
    {
        joy = FindObjectOfType<Joystick>();
    }

    // Update is called once per frame
    void Update()
    {
        if (joy == null)
        {
            joy = FindObjectOfType<Joystick>();
        }

        if (joyGambiarra < joy.Vertical)
        {
            if (joy.Vertical >= 0.8f && abriPorta)
            {
                //SceneManager.LoadScene("HUB");
                Debug.Log("Colidiu");
                OpenDoorTutorial();
            }
        }

        joyGambiarra = joy.Vertical;

    }

    public void OpenDoorTutorial()
    {

        SceneManager.LoadScene("HUB");

        FindObjectOfType<PauseManager>().VoltaMenu();
    }
}
