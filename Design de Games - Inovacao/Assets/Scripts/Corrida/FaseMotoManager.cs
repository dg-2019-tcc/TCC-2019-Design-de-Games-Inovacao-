using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FaseMotoManager : MonoBehaviour
{

	public Vector3 speed;

	public BoolVariable partidaComecou;
	
   
    void Update()
    {
		if (partidaComecou.Value)
		{
			transform.position += -speed;
		}
    }
	
}
