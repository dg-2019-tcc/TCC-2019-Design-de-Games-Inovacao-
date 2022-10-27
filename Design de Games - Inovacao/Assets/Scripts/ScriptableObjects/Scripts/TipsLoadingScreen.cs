using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TipsLoadingScreen : MonoBehaviour
{
    public Tips tips;
    private int tipsIndex;

    public TMP_Text texto;

    private void OnEnable()
    {
        tipsIndex = Random.Range(0, tips.text.Length);

        texto.text = tips.text[tipsIndex];
    }

}
