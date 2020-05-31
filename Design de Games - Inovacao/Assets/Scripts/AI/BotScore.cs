using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BotScore : MonoBehaviour
{
    private TMP_Text score;

    public FloatVariable botScore;


	private void Start()
	{
		score = GetComponentInChildren<TMP_Text>();
		FindObjectOfType<ScoreManager>().playerListEntries.Add(2, gameObject);
	}
	private void Update()
    {
        score.text = botScore.Value.ToString();
    }
}
