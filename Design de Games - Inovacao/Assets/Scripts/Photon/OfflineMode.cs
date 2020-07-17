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
    public BoolVariable demo;

    private void Awake()
    {
        acabou01 = Resources.Load<BoolVariableArray>("Acabou01");
        demo = Resources.Load<BoolVariable>("Demo");

        DontDestroyOnLoad(eu);

        if (acabou01.Value[8] == false || demo.Value == true)
        {
            modoDoOffline = true;
        }

        else
        {
            modoDoOffline = false;
        }

        PhotonNetwork.OfflineMode = modoDoOffline;

		LoadGame();
		StartCoroutine(SaveGame());
    }

    public void AtivaOffline()
    {
        PhotonNetwork.OfflineMode = true;
        modoDoOffline = PhotonNetwork.OfflineMode;
    }


	private IEnumerator SaveGame()
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
	}
}