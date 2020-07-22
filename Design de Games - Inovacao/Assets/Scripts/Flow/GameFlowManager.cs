using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

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

    public BoolVariableArray aiGanhou;

    public GameObject teste;

    public GameObject[] npcs;

    public TV tv;
    public bool ativouFase;

    private bool liberou;
    private bool resetou;


	private string sceneName;

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;

        if (aiGanhou == null)
        {
            aiGanhou = Resources.Load<BoolVariableArray>("AIGanhou");
        }

        if (sceneName == "HUB")
        {
            if (aiGanhou.Value[index] == true)
            {
                DestroyNpcs(index - 2);
            }
            else
            {
                if (index > 1)
                {
                    DestroyNpcs(index);
                }
            }
        }

    }



    public void OfflineButtonOn(bool isOn)
    {
        offlineButton.SetActive(isOn);
    }

    public void IsOffline(bool isOff)
    {
        PhotonNetwork.OfflineMode = isOff;
        OfflineMode.modoDoOffline = isOff;
    }

    public void AtivaFase(int level)
    {
        PlayerPrefs.SetInt("GanhouDoKlay", 0);
        FechaTudo();
        switch (level)
        {
            case 9 :
                Completo();
                break;

            case 8:
                //FaseRoupa();
                Completo();
                break;

            case 7:
                FaseCorrida();
                break;

            case 6:
                FaseRoupa();
                FaseCabelo();
                break;

            case 5:
                FaseMoto();
                break;


            case 4:
                FaseFutebol();
                break;

            case 3:
                FaseTenis();
                break;

            case 2:
                FaseColeta();
                break;

        }
        tv.precisaFalar = false;
      //  Debug.Log("Flow");
        ativouFase = true;
    }

    public void DestroyNpcs(int n)
    {
        for(int i = 0; i < n; i++)
        {
            Destroy(npcs[i]);
        }
    }

    public void FaseColeta()
    {
        GameManager.Instance.fase = GameManager.Fase.Coleta;

        portas[0].SetActive(true);
        doorBlock[0].SetActive(false);

    }

    public void FaseTenis()
    {
        GameManager.Instance.fase = GameManager.Fase.Tenis;

        portas[0].SetActive(false);
        portas[1].SetActive(true);

        doorBlock[0].SetActive(true);
        doorBlock[1].SetActive(false);
    }

    public void FaseFutebol()
    {
        GameManager.Instance.fase = GameManager.Fase.Futebol;

        portas[0].SetActive(false);
        portas[1].SetActive(false);
        portas[2].SetActive(true);

        doorBlock[0].SetActive(true);
        doorBlock[1].SetActive(true);
        doorBlock[2].SetActive(false);

    }

    public void FaseMoto()
    {
        GameManager.Instance.fase = GameManager.Fase.Moto;

        portas[0].SetActive(false);
        portas[1].SetActive(false);
        portas[2].SetActive(false);
        portas[3].SetActive(true);

        doorBlock[0].SetActive(true);
        doorBlock[1].SetActive(true);
        doorBlock[2].SetActive(true);
        doorBlock[3].SetActive(false);
        
    }

    public void FaseCabelo()
    {
        GameManager.Instance.fase = GameManager.Fase.Cabelo;

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

    public void FaseCorrida()
    {
        GameManager.Instance.fase = GameManager.Fase.Corrida;

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

    public void FaseRoupa()
    {
        GameManager.Instance.fase = GameManager.Fase.Bazar;

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

    public void Completo()
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
        
    }

    public void FechaTudo()
    {
        ativouFase = false;

        for(int i = 0; i < portas.Length; i++)
        {
            portas[i].SetActive(false);
            doorBlock[i].SetActive(true);
        }
    }

    public void VoltaMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

}
