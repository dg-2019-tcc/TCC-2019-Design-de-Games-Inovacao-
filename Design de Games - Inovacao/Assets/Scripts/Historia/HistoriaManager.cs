 using System.Collections;
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

    public float nextIndex;


    private void Awake()
    {
        acabou01 = Resources.Load<BoolVariable>("Acabou01");
        flowIndex = Resources.Load<FloatVariable>("FlowIndex");

        flowIndex.Value = nextIndex;

        StartCoroutine("StartHist");
    }

    public void SkipHist()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);

        SceneManager.LoadScene(nomeDoMenu);
        //AjustaFlowIndex();
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
