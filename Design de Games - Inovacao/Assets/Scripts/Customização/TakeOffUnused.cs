using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOffUnused : MonoBehaviour
{
	public bool letThemBeOn = false;


    //Está sendo chamado pelo script Player2DAnimations
	public void CheckAndExecute()
	{
		if (letThemBeOn)
		{
			return;

		}

		foreach (Transform part in GetComponentsInChildren<Transform>(true))
		{
			ChangeMultipleCustom changeMultipleCustom = part.GetComponent<ChangeMultipleCustom>();
			if (!part.gameObject.activeSelf && changeMultipleCustom != null)
			{

                foreach (GameObject subPart in changeMultipleCustom.multipleCustom)
                {
                    Destroy(subPart);
                }

                foreach(GameObject subStroke in changeMultipleCustom.multipleStroke)
                {
                    if (!subStroke.gameObject.activeSelf)
                    {
                        Destroy(subStroke);
                    }
                }
            }

        }
        letThemBeOn = true;
        Debug.Log("CheckAndExecute");
	}
}
