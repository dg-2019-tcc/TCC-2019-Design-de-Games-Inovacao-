using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;

using Photon.Pun.UtilityScripts;
using Photon.Pun.Demo.Asteroids;


public class ScoreManager : MonoBehaviourPunCallbacks
{
    public GameObject PlayerOverviewEntryPrefab;

	public Sprite[] scoreBackground;

	//[HideInInspector]
    public Dictionary<int, GameObject> playerListEntries;

    #region UNITY

    public void Awake()
    {
        playerListEntries = new Dictionary<int, GameObject>();

        foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList)
        {
            GameObject entry = Instantiate(PlayerOverviewEntryPrefab);
            entry.transform.SetParent(gameObject.transform);
            entry.transform.localScale = Vector3.one;
            //entry.GetComponentInChildren<TMP_Text>().color = AsteroidsGame.GetColor(p.GetPlayerNumber());
			entry.GetComponentInChildren<TMP_Text>().text = p.GetScore().ToString(); //string.Format("{0}\nScore: {1}", p.NickName, p.GetScore());
            playerListEntries.Add(p.ActorNumber, entry);
			entry.GetComponentInChildren<Image>().sprite = scoreBackground[p.ActorNumber-1];
			p.SetScore(0);
        }
    }

	private void OnDestroy()
	{
		PhotonNetwork.LocalPlayer.SetScore(0);
	}
	#endregion

	#region PUN CALLBACKS

	public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Destroy(playerListEntries[otherPlayer.ActorNumber].gameObject);
        playerListEntries.Remove(otherPlayer.ActorNumber);
		otherPlayer.SetScore(0);
    }

    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, Hashtable changedProps)
    {
        GameObject entry;
        if (playerListEntries.TryGetValue(targetPlayer.ActorNumber, out entry))
        {
            entry.GetComponentInChildren<TMP_Text>().text = targetPlayer.GetScore().ToString(); // string.Format("{0}\nScore: {1}", targetPlayer.NickName, targetPlayer.GetScore());
        }
    }

    #endregion
}
