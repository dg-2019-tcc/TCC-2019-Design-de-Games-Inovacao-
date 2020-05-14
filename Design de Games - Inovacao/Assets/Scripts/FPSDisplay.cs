using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FPSDisplay : MonoBehaviour
{
    public int avgFrameRate;
    public Text display_Text;

    float deltaTime = 0.0f;

    /*public void Update()
    {

        //float current = 0;
        float current = (int)(1f / Time.unscaledDeltaTime);
        avgFrameRate = (int)current;
        display_Text.text = avgFrameRate.ToString() + " FPS";

    }*/
}
