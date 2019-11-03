using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;
using Photon.Pun;

public class EmotesManager : MonoBehaviour
{

	public GameObject pause;
	public GameObject[] emote;

	public void Sticker(int index)
	{
		pause.SetActive(false);
		gameObject.GetComponent<PhotonView>().RPC("MandaSticker", RpcTarget.All, index);

	}

	[PunRPC]
	private void MandaSticker(int index)
	{

		emote[index].SetActive(true);

	}

}
