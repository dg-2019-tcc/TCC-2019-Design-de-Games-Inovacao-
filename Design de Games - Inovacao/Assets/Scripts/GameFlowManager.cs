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
    private int index;

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

        index = PlayerPrefs.GetInt("Fase");
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

        // Desativei pq se nao roda a fala do mc
        /*if (CameraShowoff != null)
        {
            CameraShowoff.SetActive(false);
        }*/
        if (buildProfs == false)
        {
            if (index < 8)
            {
                if (SceneManager.GetActiveScene().name == "MenuPrincipal")
                {
                    offlineButton.SetActive(false);
                    jogarButton.SetActive(true);

                    PhotonNetwork.OfflineMode = true;
                    OfflineMode.modoDoOffline = true;
                }


                if (SceneManager.GetActiveScene().name == "HUB")
                {
                    PhotonNetwork.OfflineMode = true;
                    OfflineMode.modoDoOffline = true;

                    if (aiGanhou.Value[0] == true)
                    {
                        AtivaFase(index);
                    }
                    else
                    {
                        FechaTudo();
                    }
                }
            }

            else
            {
                if (SceneManager.GetActiveScene().name == "MenuPrincipal")
                {
                    offlineButton.SetActive(true);
                }

                if (SceneManager.GetActiveScene().name == "HUB")
                {
                    Completo();
                }

            }
        }

        else
        {
            if (SceneManager.GetActiveScene().name == "MenuPrincipal")
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

            if (SceneManager.GetActiveScene().name == "HUB")
            {
                Completo();
                Acabou();
            }
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "HUB")
        {
            if (tv.faloComTV == true && ativouFase == false)
            {
                AtivaFase(index);
            }
        }
    }

    void AtivaFase(int faseIndex)
    {
        switch (faseIndex)
        {
            case 8:
                Completo();
                Acabou();
                break;

            case 7:
                FaseRoupa();
                break;

            case 6:
                FaseCorrida();
                break;

            case 5:
                FaseCabelo();
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
        fasesSave.fases[0] = true;
        flowIndex.Value = 1;
        FaseColeta();
    }

    public void FaseColeta()
    {
        if (fasesSave.fases[3] == false)
        {
            /*if (aiGanhou.Value == false)
            {
                CameraShowoff.SetActive(true);
            }*/

            fasesSave.fases[1] = true;

            portas[0].SetActive(true);

            doorBlock[0].SetActive(false);
        }

        else
        {
            FaseFutebol();
        }
    }

    public void FaseTenis()
    {
        Destroy(npcs[0]);

        fasesSave.fases[2] = true;

        portas[0].SetActive(false);
        portas[1].SetActive(true);

        doorBlock[0].SetActive(true);
        doorBlock[1].SetActive(false);
    }

    public void FaseFutebol()
    {
        Destroy(npcs[1]);

        if (fasesSave.fases[4] == false)
        {
            fasesSave.fases[3] = true;

            portas[0].SetActive(false);
            portas[1].SetActive(false);
            portas[2].SetActive(true);

            doorBlock[0].SetActive(true);
            doorBlock[1].SetActive(true);
            doorBlock[2].SetActive(false);
        }

        else
        {
            FaseMoto();
        }
    }

    public void FaseMoto()
    {
        Destroy(npcs[2]);
        if (fasesSave.fases[6] == false)
        {
            fasesSave.fases[4] = true;

            portas[0].SetActive(false);
            portas[1].SetActive(false);
            portas[2].SetActive(false);
            portas[3].SetActive(true);

            doorBlock[0].SetActive(true);
            doorBlock[1].SetActive(true);
            doorBlock[2].SetActive(true);
            doorBlock[3].SetActive(false);
        }
    }

    public void FaseCabelo()
    {
        Destroy(npcs[3]);
        fasesSave.fases[5] = true;

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
        if (fasesSave.fases[8] == false)
        {
            fasesSave.fases[6] = true;

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

        else
        {
            Acabou();
            Completo();
        }
    }

    public void FaseRoupa()
    {
        Destroy(npcs[5]);
        fasesSave.fases[7] = true;

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

        portas[0].SetActive(true);
        portas[1].SetActive(true);
        portas[2].SetActive(true);
        portas[3].SetActive(true);
        portas[4].SetActive(true);
        portas[5].SetActive(true);
        portas[6].SetActive(true);
        portas[7].SetActive(true);

        doorBlock[0].SetActive(false);
        doorBlock[1].SetActive(false);
        doorBlock[2].SetActive(false);
        doorBlock[3].SetActive(false);
        doorBlock[4].SetActive(false);
        doorBlock[5].SetActive(false);
        doorBlock[6].SetActive(false);
        doorBlock[7].SetActive(false);
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
