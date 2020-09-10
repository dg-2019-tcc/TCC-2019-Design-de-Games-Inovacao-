using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    public static FPSDisplay instance;
    public TMP_Text texto;
    public TMP_Text gcText;
    float deltaTime = 0.0f;

    public static bool gcOn;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        if (gcOn) { gcText.text = "GC is On"; }
        else { gcText.text = "GC is Off"; }
    }

    void OnGUI()
    {

        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);

        texto.text = text;
    }
}
