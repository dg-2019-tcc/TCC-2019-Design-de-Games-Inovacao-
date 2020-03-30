using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MotoFlip : MonoBehaviour
{
	public bool leftDir;

	public EmpinaMoto script;
	
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

    }

	


	

	
}
