using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMultipleCustom : MonoBehaviour
{
    public GameObject[] multipleCustom;


    [PunRPC]
    public void ChangeCustom(bool isActive)
    {
        if (isActive)
        {
            for(int i = 0; i < multipleCustom.Length; i++)
            {
                multipleCustom[i].SetActive(true);
                Debug.Log("Ativa");
            }
        }

        else
        {
            for (int i = 0; i < multipleCustom.Length; i++)
            {
                multipleCustom[i].SetActive(false);
            }
        }
    }
}
