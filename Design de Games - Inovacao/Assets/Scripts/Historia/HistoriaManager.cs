 using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HistoriaManager : MonoBehaviour
{
    public Image histImage;
    public Sprite[] histSprites;

    private string nomeDoMenu;

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

        AjustaFlowIndex();
    }

    public void AjustaFlowIndex()
    {
        if (flowIndex.Value == 6)
        {
            acabou01.Value = true;
            flowIndex.Value = 8;
            nomeDoMenu = "MenuPrincipal";
        }

        else if (flowIndex.Value == 4)
        {
            flowIndex.Value = 6;
            nomeDoMenu = "HUB"; ;
        }

        else if (flowIndex.Value == 3)
        {
            flowIndex.Value = 4;
            nomeDoMenu = "HUB";
        }

        else if (flowIndex.Value == 1)
        {
            flowIndex.Value = 3;
            nomeDoMenu = "HUB";
        }
        else
        {
            nomeDoMenu = "Customiza";
        }

        SceneManager.LoadScene(nomeDoMenu);
        StopCoroutine("StartHist");
    }

    IEnumerator StartHist()
    {
        for (int i = 0; i < histSprites.Length; i++)
        {
            histImage.sprite = histSprites[i];
            yield return new WaitForSeconds(5f);
        }

        AjustaFlowIndex();

    }
}
