using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoedasCanvas : MonoBehaviour
{
    public TMP_Text text;
    private int amountCoins;

    public Points moedas;

    private void Start()
    {
        amountCoins = moedas.Value;
        text.text = "Moedas:" + amountCoins.ToString();
    }

    private void Update()
    {
        if( amountCoins != moedas.Value)
        {
            amountCoins = moedas.Value;
            text.text = "Moedas:" + amountCoins.ToString();
        }
    }

}
