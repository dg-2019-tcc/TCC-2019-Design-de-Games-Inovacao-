using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class OfflineMode : MonoBehaviour
{
    public static bool modoDoOffline = false;
    public BoolVariable demo;

    #region Singleton
    private static OfflineMode _instance;

    public static OfflineMode Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<OfflineMode>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(OfflineMode).Name;
                    _instance = go.AddComponent<OfflineMode>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion


    public void AtivaOffline(bool offMode)
    {
        PhotonNetwork.OfflineMode = offMode;
        modoDoOffline = PhotonNetwork.OfflineMode;
    }
}