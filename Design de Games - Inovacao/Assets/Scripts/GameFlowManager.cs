using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

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

    public BoolVariable acabou01;

	public GameObject CameraShowoff;

    public BoolVariable aiGanhou;
    public BoolVariable resetaFase;
    public FasesSave fasesSave;

    public GameObject teste;

    public TV tv;
    public bool ativouFase;

    private void Start()
    {
        if(resetaFase.Value == true)
        {
            PlayerPrefs.SetInt("Fase", 0);
            resetaFase.Value = false;
        }

        index = PlayerPrefs.GetInt("Fase");
        Debug.Log(index);

        if (aiGanhou == null)
        {
            aiGanhou = Resources.Load<BoolVariable>("AIGanhou");
        }

        if (acabou01 == null)
        {
            acabou01 = Resources.Load<BoolVariable>("Acabou01");
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

        if (!acabou01.Value)
        {
            if (SceneManager.GetActiveScene().name == "MenuPrincipal" && flowIndex.Value <= 6)
            {
                offlineButton.SetActive(false);
                jogarButton.SetActive(true);
                OfflineMode.modoDoOffline = true;
            }
            

            if (SceneManager.GetActiveScene().name == "HUB")
            {
                if (aiGanhou.Value == false)
                {
                    FechaTudo();
                }
                else
                {
                    AtivaFase(index);
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

    private void Update()
    {
        if(tv.faloComTV == true && ativouFase == false)
        {
            AtivaFase(index);
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

        ativouFase = true;
    }

    public void ResetaJogo()
    {
        OfflineMode.modoDoOffline = true;
        acabou01.Value = false;
        offlineButton.SetActive(false);
        PlayerPrefs.SetInt("Fase", 0);
        flowIndex.Value = 0;
        Debug.Log("Resetou");
    }

    public void LiberaTudo()
    {
        OfflineMode.modoDoOffline = false;
        acabou01.Value = true;
        flowIndex.Value = 8;
        offlineButton.SetActive(true);
        PlayerPrefs.SetInt("Fase", 8);
        Debug.Log("Liberou");
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
        fasesSave.fases[2] = true;

        portas[0].SetActive(false);
        portas[1].SetActive(true);

        doorBlock[0].SetActive(true);
        doorBlock[1].SetActive(false);
    }

    public void FaseFutebol()
    {
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
        fasesSave.fases[8] = true;

        acabou01.Value = true;
        flowIndex.Value++;
    }

    public void Completo()
    {
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
