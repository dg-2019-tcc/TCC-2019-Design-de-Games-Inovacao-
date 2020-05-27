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


    public FloatVariable flowIndex;
    private float oldIndex;

    public BoolVariable acabou01;

	public GameObject CameraShowoff;

    public BoolVariable aiGanhou;

    private void Awake()
    {
        aiGanhou = Resources.Load<BoolVariable>("AIGanhou");
        CameraShowoff.SetActive(false);
        if (!acabou01.Value)
        {
            if(flowIndex.Value <= 6)
            {
                if (SceneManager.GetActiveScene().name == "MenuPrincipal")
                {
 
                    offlineButton.SetActive(false);
                    jogarButton.SetActive(true);
                }

                OfflineMode.modoDoOffline = true;
            }

            if (SceneManager.GetActiveScene().name == "HUB")
            {
                if(flowIndex.Value == 1)
                {
                    FaseColeta();
                    if (aiGanhou.Value == false)
                    {
                        CameraShowoff.SetActive(true);
                    }
                }

                if(flowIndex.Value == 2)
                {
                    FaseTenis();
                }

                if(flowIndex.Value == 3)
                {
                    FaseFutebol();
                }

                if(flowIndex.Value == 4)
                {
                    FaseMoto();
                }

                if (flowIndex.Value == 5)
                {
                    FaseCabelo();
                }

                if (flowIndex.Value == 6)
                {
                    FaseCorrida();
                }

                if (flowIndex.Value == 7)
                {
                    //VoltaMenu();
                    //FaseRoupa();
                }

                if(flowIndex.Value > 7)
                {
                    Completo();
                    Acabou();
                }
            }
        }

        else
        {
            Completo();
            if (SceneManager.GetActiveScene().name == "MenuPrincipal")
            {
                offlineButton.SetActive(true);
            }
        }
    }

    public void FaseColeta()
    {
        portas[0].SetActive(true);
    }

    public void FaseTenis()
    {
        portas[0].SetActive(false);
        portas[1].SetActive(true);
    }

    public void FaseFutebol()
    {
        portas[0].SetActive(false);
        portas[1].SetActive(false);
        portas[2].SetActive(true);
    }

    public void FaseMoto()
    {
        portas[0].SetActive(false);
        portas[1].SetActive(false);
        portas[2].SetActive(false);
        portas[3].SetActive(true);
    }

    public void FaseCabelo()
    {
        portas[0].SetActive(false);
        portas[1].SetActive(false);
        portas[2].SetActive(false);
        portas[3].SetActive(false);
        portas[4].SetActive(true);
    }

    public void FaseCorrida()
    {
        portas[0].SetActive(false);
        portas[1].SetActive(false);
        portas[2].SetActive(false);
        portas[3].SetActive(false);
        portas[4].SetActive(false);
        portas[5].SetActive(true);
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
