using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtiva : MonoBehaviour
{

    [SerializeField]
    public bool desativaCanvas;

	private PhotonView PV;
	public PlataformaManager[] PM;
	public GameObject canvas;

	void Start()
    {

		PV = GetComponent<PhotonView>();

		if (PV != null && PV.IsMine)
		{

            if(desativaCanvas == false)
            {
                canvas.SetActive(true);
            }



			PM = FindObjectsOfType<PlataformaManager>();
			for (int i = 0; i < PM.Length; i++)
			{


				PM[i].joyStick = FindObjectOfType<Joystick>();
			}
		}
	}
}
