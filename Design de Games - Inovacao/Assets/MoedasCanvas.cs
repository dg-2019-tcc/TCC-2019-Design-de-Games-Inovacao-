using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoedasCanvas : MonoBehaviour
{
    public TMP_Text text;
    private int amountCoins;

    public GameObject moedasDisplay;

    #region Singleton
    public static MoedasCanvas _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion


    private void Start()
    {
        PlayerPrefsManager.Instance.LoadPlayerPref("Coins");

        amountCoins = PlayerPrefsManager.Instance.prefsVariables.coins;
        text.text = "Moedas:" + amountCoins.ToString();
    }

    private void Update()
    {
        if( amountCoins != PlayerPrefsManager.Instance.prefsVariables.coins)
        {
            amountCoins = PlayerPrefsManager.Instance.prefsVariables.coins;
            text.text = "Moedas:" + amountCoins.ToString();
        }
    }

    public void MoedaCanvasIsActive(bool on)
    {
        moedasDisplay.SetActive(on);
    }
}
