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


	private void Start()
	{
		pv = GetComponent<PhotonView>();
	}

	void Update()
	{
		if (pv.IsMine && face != lastFace)
		{
			pv.RPC("TrocaCara", RpcTarget.All, face);
		}

		lastFace = face;
	}

	[PunRPC]
	void TrocaCara(int index)
	{
		playerMesh.materials[0].mainTexture = textures[index];
	}
}
