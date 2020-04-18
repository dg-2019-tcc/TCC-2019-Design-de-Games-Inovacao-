using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotScore : MonoBehaviour
{
    public Text score;

    public FloatVariable botScore;

    private void Update()
    {
        score.text ="Bot Score:" + botScore.Value.ToString();
    }
}
