﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HistoriaManager : MonoBehaviour
{
    public Image histImage;
    public Sprite[] histSprites;

    public string nomeDoMenu;

    public FloatVariable flowIndex;
    public BoolVariable acabou01;


    private void Awake()
    {
        acabou01 = Resources.Load<BoolVariable>("Acabou01");
        flowIndex = Resources.Load<FloatVariable>("FlowIndex");
        StartCoroutine("StartHist");
    }

    public void SkipHist()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);

        if (flowIndex.Value == 6)
        {
            acabou01.Value = true;
            flowIndex.Value = 8;
            SceneManager.LoadScene("MenuPrincipal");
        }
        else if (flowIndex.Value >1)
        {
            SceneManager.LoadScene("HUB");
        }
        else
        {
            SceneManager.LoadScene("Customiza");

        }
    }

    IEnumerator StartHist()
    {
        for (int i = 0; i < histSprites.Length; i++)
        {
            histImage.sprite = histSprites[i];
            yield return new WaitForSeconds(5f);
        }

        if (flowIndex.Value == 6)
        {
            acabou01.Value = true;
            flowIndex.Value = 8;
            SceneManager.LoadScene("MenuPrincipal");
        }
        else if (flowIndex.Value > 1)
        {
            SceneManager.LoadScene("HUB");
        }
        else
        {
            SceneManager.LoadScene("Customiza");

        }
    }
}
