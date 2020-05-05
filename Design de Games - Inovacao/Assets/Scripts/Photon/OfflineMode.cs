using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class OfflineMode : MonoBehaviour
{
    public static bool modoDoOffline = false;
    public GameObject eu;

    private void Awake()
    {
        DontDestroyOnLoad(eu);
        PhotonNetwork.OfflineMode = modoDoOffline;
    }

    public TextMeshProUGUI offlineText;

    public void AtivaOffline()
    {
        if(PhotonNetwork.OfflineMode == false)
        {
            PhotonNetwork.OfflineMode = true;
            offlineText.text = "Modo: Offline";
            offlineText.color = new Color32(255, 0, 0, 255);
            modoDoOffline = PhotonNetwork.OfflineMode;
        }
        else
        {
            PhotonNetwork.OfflineMode = false;
            offlineText.text = "Modo: Online";
            offlineText.color = new Color32(254, 155, 0, 255);
            modoDoOffline = PhotonNetwork.OfflineMode;
        }
    }
}
