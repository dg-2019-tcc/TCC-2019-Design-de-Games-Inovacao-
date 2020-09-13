using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class TakeOffUnused : MonoBehaviour
{
	public bool letThemBeOn = false;

    private UnityArmatureComponent armature;

    private void Start()
    {
        armature = GetComponent<UnityArmatureComponent>();
    }


    //Está sendo chamado pelo script Player2DAnimations
    public void CheckAndExecute()
	{
		if (letThemBeOn)
		{
			return;

		}

		foreach (UnityEngine.Transform part in GetComponentsInChildren<UnityEngine.Transform>(true))
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
	}
}
