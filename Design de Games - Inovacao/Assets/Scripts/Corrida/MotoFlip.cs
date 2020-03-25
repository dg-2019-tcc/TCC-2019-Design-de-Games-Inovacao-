using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MotoFlip : MonoBehaviour
{
	public bool leftDir;

	public BoolVariable buttonPressed;

	private bool isEmpinando;

	private void Start()
	{
		isEmpinando = false;
	}
	void Update()
    {
		leftDir = PlayerMovement.leftDir;
		if (leftDir)
		{
			transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
		}
		else
		{
			transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
		}


		if (buttonPressed)
		{
			daGrau();
		}
    }


	[PunRPC]
	public void daGrau()
	{
		if (isEmpinando)
		{
			StartCoroutine("Empinando");
			isEmpinando = true;
		}
	}


	

	private IEnumerator Empinando()
	{
		transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 45);
		buttonPressed.Value = false;
		yield return new WaitForSeconds(2);
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0), 0.5f);
		isEmpinando = false;
	}
}
