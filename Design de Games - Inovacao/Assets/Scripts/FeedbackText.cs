using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FeedbackText : MonoBehaviour
{

    public TextMeshProUGUI feedbaackText;

    public BoolVariable aiGanhou;
    public BoolVariable playerGanhou;

    void Start()
    {
        aiGanhou = Resources.Load<BoolVariable>("AIGanhou");
        playerGanhou = Resources.Load<BoolVariable>("PlayerGanhou");

        feedbaackText.text = "";
    }

    void Update()
    {
        if(aiGanhou.Value == true)
        {
            feedbaackText.text = "Você perdeu... =( Tente mais uma vez.";
        }

        else if (playerGanhou.Value == true)
        {
            feedbaackText.text = "Parabéns, você ganhou!!! =)";
        }
    }
}
