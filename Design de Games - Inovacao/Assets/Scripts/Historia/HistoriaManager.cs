 using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityCore.Scene;

public class HistoriaManager : MonoBehaviour
{
    public Image histImage;
    public Sprite histImageColeta;
    public Sprite histImageFutebol;
    public Sprite histImageMoto;
    public Sprite[] histImageCorrida;
    public Sprite[] histSprites;


    public string nomeDoMenu;

    public float flowIndex;
    public BoolVariable demo;

    public int nextIndex;

    bool histInicial;

    private string sceneName;
    public SceneType nextScene;


    private void Awake()
    {
        demo = Resources.Load<BoolVariable>("Demo");
        flowIndex = nextIndex;
        int debug = PlayerPrefs.GetInt("Fase");
        sceneName = SceneManager.GetActiveScene().name;

        if (sceneName != "FimDemo")
        {
            StartCoroutine("StartHist");
        }
    }

    public void SkipHist()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        StopCoroutine("StartHist");
        //SceneManager.LoadScene(nomeDoMenu);
        LoadingManager.instance.LoadNewScene(nextScene, SceneType.MenuPrincipal, false);


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
        switch(GameManager.Instance.sceneOld)
        {
            //case GameManager.Fase.Start:
            case SceneType.MenuPrincipal:
                nomeDoMenu = "Customiza";
                nextScene = SceneType.Customiza;
                for (int i = 0; i < histSprites.Length; i++)
                {
                    histImage.sprite = histSprites[i];
                    yield return new WaitForSeconds(5f);
                }
                break;

            //case GameManager.Fase.Coleta:
            case SceneType.Coleta:
                nomeDoMenu = "HUB";
                nextScene = SceneType.HUB;
                histImage.sprite = histImageColeta;
                yield return new WaitForSeconds(5f);
                break;

            //case GameManager.Fase.Futebol:
            case SceneType.Futebol:
                nomeDoMenu = "HUB";
                nextScene = SceneType.HUB;
                histImage.sprite = histImageFutebol;
                yield return new WaitForSeconds(5f);
                break;

            //case GameManager.Fase.Moto:
            case SceneType.Moto:
                if (demo.Value)
                {
                    if (sceneName == "Historia")
                    {
                        nomeDoMenu = "FimDemo";
                        nextScene = SceneType.FimDemo;
                    }
                    if(sceneName == "FimDemo")
                    {
                        nomeDoMenu = "MenuPrincipal";
                        nextScene = SceneType.MenuPrincipal;
                    }
                }
                else
                {
                    nomeDoMenu = "HUB";
                    nextScene = SceneType.HUB;
                }
                histImage.sprite = histImageMoto;
                yield return new WaitForSeconds(5f);
                break;

            //case GameManager.Fase.Corrida:
            case SceneType.Corrida:
                nomeDoMenu = "HUB";
                nextScene = SceneType.HUB;
                for (int i = 0; i < histImageCorrida.Length; i++)
                {
                    histImage.sprite = histImageCorrida[i];
                    yield return new WaitForSeconds(5f);
                }
                break;
        }

        LoadingManager.instance.LoadNewScene(nextScene, SceneType.MenuPrincipal, false);
        //SceneManager.LoadScene(nomeDoMenu);
    }
}
