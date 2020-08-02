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
        //Debug.Log("O modo Offline está ativo é" + PhotonNetwork.OfflineMode);
    }


	/*private IEnumerator SaveGame()
	{
		yield return new WaitForSeconds(30);
		for (int i = 0; i < acabou01.Value.Length; i++)
		{
			PlayerPrefs.SetInt("Acabou01" + i.ToString(), boolToInt(acabou01.Value[i]));
				
		}

		StartCoroutine(SaveGame());
	}

	private void LoadGame()
	{
		for (int i = 0; i < acabou01.Value.Length; i++)
		{
			acabou01.Value[i] = intToBool(PlayerPrefs.GetInt("Acabou01" + i.ToString()));
		}
	}



	private int boolToInt(bool val)
	{
		if (val)
			return 1;
		else
			return 0;
	}

	private bool intToBool(int val)
	{
		if (val != 0)
			return true;
		else
			return false;
	}*/
}