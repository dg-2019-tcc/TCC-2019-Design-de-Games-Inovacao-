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

    public int nextIndex;


    private void Awake()
    {


        //PlayerPrefs.SetInt("Fase", nextIndex);
        flowIndex.Value = nextIndex;
        int debug = PlayerPrefs.GetInt("Fase");

        StartCoroutine("StartHist");
    }

    public void SkipHist()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        StopCoroutine("StartHist");
        flowIndex.Value = nextIndex;
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

        flowIndex.Value = nextIndex;
        SceneManager.LoadScene(nomeDoMenu);
        //AjustaFlowIndex();

    }
}
