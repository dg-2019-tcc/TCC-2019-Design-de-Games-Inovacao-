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


    public FloatVariable flowIndex;
    public int index;

    public BoolVariableArray acabou01;

	public GameObject CameraShowoff;

    public BoolVariableArray aiGanhou;
    public BoolVariable resetaFase;
    public FasesSave fasesSave;

    public GameObject teste;

    public GameObject[] npcs;

    public TV tv;
    public bool ativouFase;

    private bool liberou;
    private bool resetou;

    public OfflineMode offlineMode;

    public bool buildProfs;

    public GameObject resetaButton;
    public GameObject liberaButton;


	private string sceneName;

    private void Start()
    {
       // buildProfs = true;
        liberou = false;
        resetou = false;

        if(resetaFase.Value == true && resetaFase != null && buildProfs == false)
        {
            PlayerPrefs.SetInt("Fase", 0);
            resetaFase.Value = false;
        }


        //index = PlayerPrefs.GetInt("Fase");
        // Debug.Log(index);

        if (aiGanhou == null)
        {
            aiGanhou = Resources.Load<BoolVariableArray>("AIGanhou");
        }

        if (acabou01 == null)
        {
            acabou01 = Resources.Load<BoolVariableArray>("Acabou01");
        }

        if (fasesSave == null)
        {
            fasesSave = Resources.Load<FasesSave>("FasesSave");
        }

        if (flowIndex == null)
        {
            flowIndex = Resources.Load<FloatVariable>("FlowIndex");
        }

        for (int i = 0; i < acabou01.Value.Length; i++)
        {
            Debug.Log(index);
            if (!acabou01.Value[i])
            {
                index = i;
                Debug.Log(index);
                break;
            }

        }

		// Desativei pq se nao roda a fala do mc
		/*if (CameraShowoff != null)
        {
            CameraShowoff.SetActive(false);
        }*/

		sceneName = SceneManager.GetActiveScene().name;

		if (buildProfs == false)
        {
            if (acabou01.Value[8] == false)
            {
                if (sceneName == "MenuPrincipal")
                {
                    offlineButton.SetActive(false);
                    jogarButton.SetActive(true);

                    PhotonNetwork.OfflineMode = true;
                    OfflineMode.modoDoOffline = true;
                }


                if (sceneName == "HUB")
                {
                    /*PhotonNetwork.OfflineMode = true;
                    OfflineMode.modoDoOffline = true;*/

                    if (aiGanhou.Value[index] == true)
                    {
                        AtivaFase();
                    }
                    else
                    {
                        FechaTudo();
                    }
                }
            }

            else
            {
                if (sceneName == "MenuPrincipal")
                {
                    offlineButton.SetActive(true);
                }

                if (sceneName == "HUB")
                {
                    Acabou();
                    Completo();
                }

            }
        }

        else
        {
            if (sceneName == "MenuPrincipal")
            {
                resetaButton.SetActive(false);
                liberaButton.SetActive(false);
                LiberaTudo();
                PlayerPrefs.SetInt("Fase", 8);
                resetaFase.Value = false;

                offlineButton.SetActive(false);
                jogarButton.SetActive(true);

                PhotonNetwork.OfflineMode = true;
                OfflineMode.modoDoOffline = true;
            }

            if (sceneName == "HUB")
            {
                Completo();
                Acabou();
            }
        }
    }

    /*private void Update()
    {
        if (SceneManager.GetActiveScene().name == "HUB")
        {
            if (tv.faloComTV == true && ativouFase == false)
            {
                Debug.Log(index);
                AtivaFase(index);
            }
        }
    }*/

    public void AtivaFase()
    {
        //index = PlayerPrefs.GetInt("Fase");

        for (int i = 0; i < acabou01.Value.Length; i++)
        {

            if (!acabou01.Value[i])
            {
                index = i;
                break;
            }

        }

        switch (index)
        {
            case 9 :
                Completo();
                Acabou();
                break;

            case 8:
                FaseRoupa();
                break;

            case 7:
                FaseCorrida();
                break;

            case 6:
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

            case 0:
                Fase0();
                break;

            default:
                //FasesSave();
                Fase0();
                break;
        }
        tv.precisaFalar = false;
      //  Debug.Log("Flow");
        ativouFase = true;
    }

    public void ResetaJogo()
    {
        if (resetou == false)
        {
            PlayerPrefs.SetInt("Fase", 0);

            PhotonNetwork.OfflineMode = true;
            OfflineMode.modoDoOffline = true;

            resetaFase.Value = true;
			for (int i = 0; i < acabou01.Value.Length; i++)
			{
				acabou01.Value[i] = false;
                aiGanhou.Value[i] = false;
			}
            flowIndex.Value = 0;

            offlineButton.SetActive(false);

            resetou = true;
            liberou = false;
           // Debug.Log("Resetou");
        }
    }

    public void LiberaTudo()
    {
        if (liberou == false)
        {
            PlayerPrefs.SetInt("Fase", 8);

            offlineMode.AtivaOffline();
            /*PhotonNetwork.OfflineMode = false;
            OfflineMode.modoDoOffline = false;*/

            resetaFase.Value = false;
			for (int i = 0; i < acabou01.Value.Length; i++)
			{
				acabou01.Value[i] = true;
			}
			flowIndex.Value = 8;

            offlineButton.SetActive(true);

            liberou = true;
            resetou = false;
          //  Debug.Log("Liberou");
        }
    }



    void Fase0()
    {
        /*flowIndex.Value = 1;
        FaseColeta();*/
    }

    public void FaseColeta()
    {

            /*if (aiGanhou.Value == false)
            {
                CameraShowoff.SetActive(true);
            }*/


            portas[0].SetActive(true);

            doorBlock[0].SetActive(false);

    }

    public void FaseTenis()
    {
        Destroy(npcs[0]);


        portas[0].SetActive(false);
        portas[1].SetActive(true);

        doorBlock[0].SetActive(true);
        doorBlock[1].SetActive(false);
    }

    public void FaseFutebol()
    {
        Destroy(npcs[1]);


            portas[0].SetActive(false);
            portas[1].SetActive(false);
            portas[2].SetActive(true);

            doorBlock[0].SetActive(true);
            doorBlock[1].SetActive(true);
            doorBlock[2].SetActive(false);

    }

    public void FaseMoto()
    {
        //Destroy(npcs[2]);

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
        Destroy(npcs[3]);

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
        Destroy(npcs[4]);


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
        Destroy(npcs[5]);

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

    public void Acabou()
    {
        Destroy(npcs[6]);
        fasesSave.fases[8] = true;

        acabou01.Value[0] = true;
        flowIndex.Value++;
    }

    public void Completo()
    {
        for(int i = 0; i< npcs.Length; i++)
        {
            Destroy(npcs[i]);
        }

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
