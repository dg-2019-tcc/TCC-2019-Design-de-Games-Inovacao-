 using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HistoriaManager : MonoBehaviour
{
    public Image histImage;
    public Sprite histImageColeta;
    public Sprite histImageFutebol;
    public Sprite histImageMoto;
    public Sprite[] histImageCorrida;
    public Sprite[] histSprites;


    public string nomeDoMenu;

    public FloatVariable flowIndex;
    public BoolVariable demo;

    public int nextIndex;

    bool histInicial;

    private string sceneName;


    private void Awake()
    {
        demo = Resources.Load<BoolVariable>("Demo");
        flowIndex.Value = nextIndex;
        int debug = PlayerPrefs.GetInt("Fase");
        sceneName = SceneManager.GetActiveScene().name;

        StartCoroutine("StartHist");
    }

    public void SkipHist()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        StopCoroutine("StartHist");

        SceneManager.LoadScene(nomeDoMenu);
    }

    void HistInicial()
    {
        for (int i = 0; i < histSprites.Length; i++)
        {
            histImage.sprite = histSprites[i];
        }
    }

    void HistColeta()
    {
        histImage.sprite = histImageColeta;
    }

    void HistFutebol()
    {
        histImage.sprite = histImageFutebol;
    }

    void HistMoto()
    {
        histImage.sprite = histImageMoto;
    }
    void HistCorrida()
    {
        for(int i = 0; i < histImageCorrida.Length; i++)
        {
            histImage.sprite = histImageCorrida[i];
        }
    }



    IEnumerator StartHist()
    {
        switch(GameManager.Instance.fase)
        {
            case GameManager.Fase.Start:
                nomeDoMenu = "Customiza";
                for (int i = 0; i < histSprites.Length; i++)
                {
                    histImage.sprite = histSprites[i];
                    yield return new WaitForSeconds(5f);
                }
                break;

            case GameManager.Fase.Coleta:
                nomeDoMenu = "HUB";
                histImage.sprite = histImageColeta;
                yield return new WaitForSeconds(5f);
                break;

            case GameManager.Fase.Futebol:
                nomeDoMenu = "HUB";
                histImage.sprite = histImageFutebol;
                yield return new WaitForSeconds(5f);
                break;

            case GameManager.Fase.Moto:
                if (demo.Value)
                {
                    if (sceneName == "Historia")
                    {
                        nomeDoMenu = "FimDemo";
                    }
                    if(sceneName == "FimDemo")
                    {
                        nomeDoMenu = "MenuPrincipal";
                    }
                }
                else
                {
                    nomeDoMenu = "HUB";
                }
                histImage.sprite = histImageMoto;
                yield return new WaitForSeconds(5f);
                break;

            case GameManager.Fase.Corrida:
                nomeDoMenu = "HUB";
                for (int i = 0; i < histImageCorrida.Length; i++)
                {
                    histImage.sprite = histImageCorrida[i];
                    yield return new WaitForSeconds(5f);
                }
                break;
        }


        SceneManager.LoadScene(nomeDoMenu);
    }
}
