using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    public static FPSDisplay instance;
    public TMP_Text texto;
    float deltaTime = 0.0f;

    float fps;
    string text;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

    }

    void OnGUI()
    {

        fps = 1.0f / deltaTime;
        text = fps.ToString(); ;

        texto.text = text;
    }
}
