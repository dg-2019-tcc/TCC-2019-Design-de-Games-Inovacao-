﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMarioSpriteMaskTransition : MonoBehaviour
{
	public BoolVariable close;

	public GameObject maskedSprite;

    void Start()
    {
		close.Value = false;
    }

	private void Update()
	{
		CloseSpotlight();
	}


	void CloseSpotlight()
	{
		if (close.Value)
		{
			maskedSprite.SetActive(true);
			if (transform.localScale.x > 0.5f)
			{
				transform.localScale -= new Vector3(0.3f, 0.3f, 0.3f);
			}
			else
			{
				transform.localScale = new Vector3(0, 0, 0);
			}
		}
		else
		{
			if (transform.localScale.x < 7)
			{
				transform.localScale += new Vector3(0.3f, 0.3f, 0.3f);
			}
			else
			{
				maskedSprite.SetActive(false);
			}
		}
	}
}
