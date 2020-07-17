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

    public OfflineMode offlineMode;

	private string sceneName;

    private void Start()
    {
       // buildProfs = true;
        liberou = false;
        resetou = false;

        offlineMode = FindObjectOfType<OfflineMode>();


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

    public void OnlineMode(bool isOn)
    {
        PhotonNetwork.OfflineMode = isOn;
        OfflineMode.modoDoOffline = isOn;
    }

    public void AtivaFase(int level)
    {
        switch (index)
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
        portas[0].SetActive(true);
        doorBlock[0].SetActive(false);

    }

    public void FaseTenis()
    {
        portas[0].SetActive(false);
        portas[1].SetActive(true);

        doorBlock[0].SetActive(true);
        doorBlock[1].SetActive(false);
    }

    public void FaseFutebol()
    {

        portas[0].SetActive(false);
        portas[1].SetActive(false);
        portas[2].SetActive(true);

        doorBlock[0].SetActive(true);
        doorBlock[1].SetActive(true);
        doorBlock[2].SetActive(false);

    }

    public void FaseMoto()
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

    public void FaseCabelo()
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

    public void FaseCorrida()
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

    public void FaseRoupa()
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

        portas[0].SetActive(false);
        portas[1].SetActive(false);
        portas[2].SetActive(false);
        portas[3].SetActive(false);
        portas[4].SetActive(false);
        portas[5].SetActive(false);
        portas[6].SetActive(false);
        portas[7].SetActive(false);

        doorBlock[0].SetActive(true);
        doorBlock[1].SetActive(true);
        doorBlock[2].SetActive(true);
        doorBlock[3].SetActive(true);
        doorBlock[4].SetActive(true);
        doorBlock[5].SetActive(true);
        doorBlock[6].SetActive(true);
        doorBlock[7].SetActive(true);
    }

    public void VoltaMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

}
