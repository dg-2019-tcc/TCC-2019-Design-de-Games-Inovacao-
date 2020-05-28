using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateManager : MonoBehaviour
{
    public int target = 30;

    public GameObject eu;

    private void Start()
    {
        DontDestroyOnLoad(eu);
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
    }

    private void Update()
    {
        if(target != Application.targetFrameRate)
        {
            Application.targetFrameRate = target;
        }
    }
}
