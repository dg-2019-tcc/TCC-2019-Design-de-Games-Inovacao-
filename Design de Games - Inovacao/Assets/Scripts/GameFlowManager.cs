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
    private float oldIndex;

    public BoolVariable acabou01;

    public BoolVariable ganhouColeta;
    public BoolVariable ganhouCorrida;
    public BoolVariable ganhouFutebol;
    public BoolVariable ganhouMoto;

	public GameObject CameraShowoff;

    public BoolVariable aiGanhou;

    public GameObject teste;

    private void Awake()
    {
        aiGanhou = Resources.Load<BoolVariable>("AIGanhou");
        acabou01 = Resources.Load<BoolVariable>("Acabou01");
        ganhouColeta = Resources.Load<BoolVariable>("GanhouColeta");
        ganhouCorrida = Resources.Load<BoolVariable>("GanhouCorrida");
        ganhouFutebol = Resources.Load<BoolVariable>("GanhouFutebol");
        ganhouMoto = Resources.Load<BoolVariable>("GanhouMoto");
        flowIndex = Resources.Load<FloatVariable>("FlowIndex");

        if (CameraShowoff != null)
        {
            CameraShowoff.SetActive(false);
        }

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
                AtivaFase();
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

    void AtivaFase()
    {
        switch (flowIndex.Value)
        {
            case 8:
                Completo();
                Acabou();
                break;

            case 6:
                FaseCorrida();
                break;

            case 4:
                FaseMoto();
                break;

            case 3:
                FaseFutebol();
                break;

            case 1:
                FaseColeta();
                break;

            default:
                teste.SetActive(true);
                break;
        }
    }

    void Fase0()
    {
        flowIndex.Value = 1;
        FaseColeta();
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

    public void Acabou()
    {
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

    public void VoltaMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    private void LateUpdate()
    {
        oldIndex = flowIndex.Value;
    }

}
