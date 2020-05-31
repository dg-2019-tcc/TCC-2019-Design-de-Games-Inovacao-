﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FeedbackText : MonoBehaviour
{

	public Image FeedbackImage;

	public Sprite winSprite;
	public Sprite loseSprite;

	public BoolVariable aiGanhou;
    public BoolVariable playerGanhou;

    private int ganhouColeta;

    void Start()
    {
		FeedbackImage = GetComponent<Image>();
        aiGanhou = Resources.Load<BoolVariable>("AIGanhou");
        playerGanhou = Resources.Load<BoolVariable>("PlayerGanhou");

		FeedbackImage.enabled = false;
    }

    void Update()
    {
        if(aiGanhou.Value == true)
        {
			FeedbackImage.enabled = true;
			FeedbackImage.sprite = loseSprite;
        }

        else if (playerGanhou.Value == true)
        {
            ganhouColeta = PlayerPrefs.GetInt("GanhouColeta");

            if (ganhouColeta == 1)
            {
                FeedbackImage.enabled = true;
                FeedbackImage.sprite = winSprite;
            }

            else
            {
                FeedbackImage.enabled = true;
                FeedbackImage.sprite = loseSprite;
            }
		}
    }
}
