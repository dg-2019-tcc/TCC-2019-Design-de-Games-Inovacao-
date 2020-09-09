using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoedaFeedbackLerp : MonoBehaviour
{
    public static MoedaFeedbackLerp instance;
    public DOTweenUI tweenUI;
    public GameObject[] tweenCoinPrefabs;
    private GameObject coinPrefab;
	public TMP_Text texto;
	public Points moedas;
	private float pontosDisplay;

    public GameObject objUI;

    public float timeToDisable = 5f;

    [SerializeField]
    private float timer;

    private bool showingCoin;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (showingCoin)
        {
            timer += Time.deltaTime;
            if(timer >= timeToDisable)
            {
                MoedaCanvasIsActive(false);
                timer = 0;
            }
        }
    }
    public void MoedaCanvasIsActive(bool on, int coinType = 0)
    {
        showingCoin = on;
        if (on == true)
        {
            timer -= 3;
            //objUI.SetActive(true);
            ShowMoedas();
            tweenUI.TweenIn();
            if (coinType != 0)
            {
                switch (coinType)
                {
                    case 1:
                        coinPrefab = tweenCoinPrefabs[0];
                        break;
                    case 5:
                        coinPrefab = tweenCoinPrefabs[1];
                        break;
                    case 10:
                        coinPrefab = tweenCoinPrefabs[2];
                        break;
                    case 100:
                        coinPrefab = tweenCoinPrefabs[3];
                        break;
                }

                Instantiate(coinPrefab);
            }
            //tweenCoin.SetActive(true);
        }
        else
        {
            tweenUI.TweenOut();
            //tweenCoin.SetActive(false);
        }
    }

    void ShowMoedas()
    {
        pontosDisplay = Mathf.Lerp(pontosDisplay, moedas.Value, 1f);
        texto.text = pontosDisplay.ToString();
        while(timer < timeToDisable) { return; }
    }

    
}
