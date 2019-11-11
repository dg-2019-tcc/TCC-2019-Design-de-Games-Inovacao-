using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerFaceAnimations : MonoBehaviour
{
	public SkinnedMeshRenderer playerMesh;
	public Texture[] textures;

	[Header("Index for animation")]
	public int face;

	private int lastFace;

	private PhotonView pv;



	[Header("Randomizer")]
	public bool randomizar;
	public float tempo;
	public float tempoVariacao;


	private void Start()
	{
		pv = GetComponent<PhotonView>();
		lastFace = 413;
		if(randomizar) StartCoroutine(CaraRandom());
	}

	void Update()
	{
		if (PhotonNetwork.PlayerList.Length > 1 && !pv.IsMine) return;

			if (face != lastFace)
		{
			pv.RPC("TrocaCara", RpcTarget.All, face);
			playerMesh.materials[0].mainTexture = textures[face];
		}

		lastFace = face;

		//-----------------Troca Random pra mostrar que funciona
		

	}

	IEnumerator CaraRandom()
	{
		yield return new WaitForSeconds(tempo + Random.Range(0, tempoVariacao));
		face = (int)Random.Range(0, textures.Length);
		StartCoroutine(CaraRandom());

	}
	[PunRPC]
	void TrocaCara(int index)
	{
		playerMesh.materials[0].mainTexture = textures[index];
	}
}
