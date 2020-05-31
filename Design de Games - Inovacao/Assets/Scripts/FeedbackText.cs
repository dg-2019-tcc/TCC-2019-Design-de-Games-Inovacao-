using System.Collections;
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


    public void Ganhou()
    {
        FeedbackImage.enabled = true;
        FeedbackImage.sprite = winSprite;
    }

    public void Perdeu()
    {
        FeedbackImage.enabled = true;
        FeedbackImage.sprite = loseSprite;
    }
}
