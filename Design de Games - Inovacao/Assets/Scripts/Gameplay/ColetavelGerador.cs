using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ColetavelGerador : MonoBehaviour
{
    public GameObject[] coletaveis;
	private int index;


	public GameObject lastColetavel;

	bool isCoroutineRunning;

    #region Unity Function

    //talvez colocar uma "seed" gerada no começo da partida, pra n ter que calcular na hora
    private void Start()
    {
        RearrangeColetavel();
        SetIndexes();
        SelectColetavel();
        coletaveis[(int)PhotonNetwork.CurrentRoom.CustomProperties["IndexColetavel"]].SetActive(true);
    }


    void Update()
    {
        //		Debug.Log((int)PhotonNetwork.CurrentRoom.CustomProperties["IndexColetavel"]);

        RearrangeColetavel();


        if (!IsThereColetavel() && !isCoroutineRunning)
        {
            SelectColetavel();

        }
    }

    #endregion

    #region Public Functions

    public void DrawProtocol()
    {
        lastColetavel.SetActive(true);
    }

    #endregion

    #region Private Functions

    private int[] fullIndex;
    void SetIndexes()
    {
        fullIndex = new int[coletaveis.Length];
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {

            for (int i = coletaveis.Length - 1; i >= 0; i--)
            {
                fullIndex[i] = (int)Random.Range(0, i - 1);
            }

            PhotonNetwork.CurrentRoom.CustomProperties["FullIndexColetaveis"] = fullIndex;
            Debug.Log(fullIndex);


        }
    }

    void SelectColetavel()
    {
        index = fullIndex[coletaveis.Length - 1];
        PhotonNetwork.CurrentRoom.CustomProperties["IndexColetavel"] = index;
        StartCoroutine(ActivateRightColetavel());


    }

    private IEnumerator ActivateRightColetavel()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < coletaveis.Length - 1; i++)
        {
            coletaveis[i].SetActive(false);
        }
        coletaveis[(int)PhotonNetwork.CurrentRoom.CustomProperties["IndexColetavel"]].SetActive(true);
        isCoroutineRunning = false;
    }

    void RearrangeColetavel()
    {
        List<GameObject> tempColetaveis = new List<GameObject>();
        foreach (GameObject item in coletaveis)
        {
            if (item != null)
                tempColetaveis.Add(item);
        }
        coletaveis = tempColetaveis.ToArray();

    }

    private bool IsThereColetavel()
    {
        for (int i = 0; i < coletaveis.Length; i++)
        {
            if (coletaveis[i].activeSelf)
            {
                return true;
            }
        }
        return false;

    }

    #endregion

}
