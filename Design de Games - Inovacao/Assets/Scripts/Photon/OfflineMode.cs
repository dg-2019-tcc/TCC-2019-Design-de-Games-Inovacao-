using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class OfflineMode : MonoBehaviour
{
    public static bool modoDoOffline = false;
    public GameObject eu;
    public BoolVariableArray acabou01;

    private void Awake()
    {
        acabou01 = Resources.Load<BoolVariableArray>("Acabou01");

        DontDestroyOnLoad(eu);

        if (acabou01.Value[8] == false)
        {
            modoDoOffline = true;
        }

        else
        {
            modoDoOffline = false;
        }

        PhotonNetwork.OfflineMode = modoDoOffline;
    }

    public void AtivaOffline()
    {
        PhotonNetwork.OfflineMode = true;
        modoDoOffline = PhotonNetwork.OfflineMode;
    }

}