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

        AjustaFlowIndex();
    }

    public void SkipHist()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);

        SceneManager.LoadScene(nomeDoMenu);
        //AjustaFlowIndex();
    }

    public void AjustaFlowIndex()
    {
        //StopCoroutine("StartHist");

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
            Debug.Log("Cosleta");
            flowIndex.Value = 3;
            nomeDoMenu = "HUB";
        }

        else if (flowIndex.Value == 0)
        {
            Debug.Log("Customiza");
            flowIndex.Value = 1;
            nomeDoMenu = "Customiza";
        }

        StartCoroutine("StartHist");
    }

    IEnumerator StartHist()
    {
        for (int i = 0; i < histSprites.Length; i++)
        {
            histImage.sprite = histSprites[i];
            yield return new WaitForSeconds(5f);
        }

        SceneManager.LoadScene(nomeDoMenu);
        //AjustaFlowIndex();

    }
}
