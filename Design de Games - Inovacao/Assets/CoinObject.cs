﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObject : MonoBehaviour
{
    public Points coin;

    public int coinValue;

    public void PegouMoeda()
    {
        coin.Add(coinValue);
        Debug.Log("Pegou a moeda");
        Destroy(gameObject);
    }
}