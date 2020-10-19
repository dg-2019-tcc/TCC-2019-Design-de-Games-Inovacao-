using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityCore.Scene;

public class GameFlowManager : MonoBehaviour
{
    //MENU PRINCIPAL
    public GameObject offlineButton;
    public GameObject jogarButton;

    //HUB
    public GameObject[] portas;
    public GameObject[] doorBlock;

    public int index;
    public int npcIndex;

    public BoolVariable demo;

    public GameObject teste;

    public GameObject[] npcs;

    public TV tv;
    public bool ativouFase;

    private bool liberou;
    private bool resetou;


	private string sceneName;

    #region Unity Function

    private void Start()
    {
        if (demo == null) { demo = Resources.Load<BoolVariable>("Demo");}
        if (GameManager.sceneAtual == SceneType.HUB)
        {
            DestroyNpcs(PlayerPrefsManager.Instance.prefsVariables.levelIndex);
        }

    }

    #endregion

    #region Public Functions

    public void OfflineButtonOn(bool isOn)
    {
        offlineButton.SetActive(isOn);
    }

    public void AtivaFase(int level)
    {
        //PlayerPrefs.SetInt("GanhouDoKlay", 0);
        PlayerPrefsManager.Instance.SavePlayerPrefs("GanhouDoKlay", 0);
        FechaTudo();
        if (demo.Value == false)
        {
            switch (level)
            {
                case 8:
                    Completo();
                    break;

                case 7:
                    //FaseRoupa();
                    //Completo();
                    FechaTudo();
                    PlayerPrefsManager.Instance.SavePlayerPrefs("LevelIndex", 8);
                    //Debug.Log(PlayerPrefsManager.Instance.prefsVariables.levelIndex);
                    break;

                case 6:
                    FaseCorrida();
                    break;

                case 5:
                    FaseRoupa();
                    //FaseCabelo();
                    break;

                case 4:
                    FaseMoto();
                    break;


                case 3:
                    FaseFutebol();
                    break;

                case 2:
                    FaseTenis();
                    break;

                case 1:
                    FaseColeta();
                    break;

            }
        }
        else
        {
            switch (level)
            {
                case 8:
                    Completo();
                    break;

                case 5:
                    AcabouDemo();
                    break;

                case 4:
                    FaseMoto();
                    break;


                case 3:
                    FaseFutebol();
                    break;

                case 2:
                    FaseTenis();
                    break;

                case 1:
                    FaseColeta();
                    break;
            }
        }
        tv.precisaFalar = false;
        //  Debug.Log("Flow");
        ativouFase = true;
    }

    public void VoltaMenu()
    {
        //SceneManager.LoadScene("MenuPrincipal");
        LoadingManager.instance.LoadNewScene(SceneType.MenuPrincipal, GameManager.sceneAtual, false);
    }

    #endregion

    #region Private Functions

    private void DestroyNpcs(int n)
    {
        for (int i = 0; i < n; i++)
        {
            Destroy(npcs[i]);
        }
    }

    private void FaseColeta()
    {
        //GameManager.Instance.fase = GameManager.Fase.Coleta;

        portas[0].SetActive(true);
        doorBlock[0].SetActive(false);

    }

    private void FaseTenis()
    {
        portas[0].SetActive(false);
        portas[1].SetActive(true);

        doorBlock[0].SetActive(true);
        doorBlock[1].SetActive(false);
    }

    private void FaseFutebol()
    {
        //GameManager.Instance.fase = GameManager.Fase.Futebol;

        portas[0].SetActive(false);
        portas[1].SetActive(false);
        portas[2].SetActive(true);

        doorBlock[0].SetActive(true);
        doorBlock[1].SetActive(true);
        doorBlock[2].SetActive(false);

    }

    private void FaseMoto()
    {
        portas[0].SetActive(false);
        portas[1].SetActive(false);
        portas[2].SetActive(false);
        portas[3].SetActive(true);

        doorBlock[0].SetActive(true);
        doorBlock[1].SetActive(true);
        doorBlock[2].SetActive(true);
        doorBlock[3].SetActive(false);

    }

    private void FaseCabelo()
    {
        portas[0].SetActive(false);
        portas[1].SetActive(false);
        portas[2].SetActive(false);
        portas[3].SetActive(false);
        portas[4].SetActive(true);

        doorBlock[0].SetActive(true);
        doorBlock[1].SetActive(true);
        doorBlock[2].SetActive(true);
        doorBlock[3].SetActive(true);
        doorBlock[4].SetActive(false);
    }

    private void FaseCorrida()
    {
        portas[0].SetActive(false);
        portas[1].SetActive(false);
        portas[2].SetActive(false);
        portas[3].SetActive(false);
        portas[4].SetActive(false);
        portas[5].SetActive(true);

        doorBlock[0].SetActive(true);
        doorBlock[1].SetActive(true);
        doorBlock[2].SetActive(true);
        doorBlock[3].SetActive(true);
        doorBlock[4].SetActive(true);
        doorBlock[5].SetActive(false);

    }

    private void FaseRoupa()
    {
        portas[0].SetActive(false);
        portas[1].SetActive(false);
        portas[2].SetActive(false);
        portas[3].SetActive(false);
        portas[4].SetActive(false);
        portas[5].SetActive(false);
        portas[6].SetActive(true);

        doorBlock[0].SetActive(true);
        doorBlock[1].SetActive(true);
        doorBlock[2].SetActive(true);
        doorBlock[3].SetActive(true);
        doorBlock[4].SetActive(true);
        doorBlock[5].SetActive(true);
        doorBlock[6].SetActive(false);
    }

    private void Completo()
    {
        DestroyNpcs(8);

        for (int i = 0; i < portas.Length; i++)
        {
            portas[i].SetActive(true);
            portas[i].SendMessage("TurnOffLocator");
        }

        for (int i = 0; i < doorBlock.Length; i++)
        {
            doorBlock[i].SetActive(false);
        }
        //Debug.Log("AtivaFAse");


    }

    private void AcabouDemo()
    {
        portas[0].SetActive(true);
        portas[1].SetActive(true);
        portas[2].SetActive(true);
        portas[3].SetActive(true);
        doorBlock[0].SetActive(false);
        doorBlock[1].SetActive(false);
        doorBlock[2].SetActive(false);
        doorBlock[3].SetActive(false);
    }

    private void FechaTudo()
    {
        ativouFase = false;

        for (int i = 0; i < portas.Length; i++)
        {
            portas[i].SetActive(false);
            doorBlock[i].SetActive(true);
        }
    }

    #endregion

}
