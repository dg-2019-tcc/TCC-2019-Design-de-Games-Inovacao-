using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotoFlip : MonoBehaviour
{
	public bool leftDir;
   
    void Update()
    {
		leftDir = PlayerMovement.leftDir;
		if (leftDir)
		{
			transform.rotation = Quaternion.Euler(0, 180, 0);
		}
		else
		{
			transform.rotation = Quaternion.Euler(0, 0, 0);
		}
    }
}
