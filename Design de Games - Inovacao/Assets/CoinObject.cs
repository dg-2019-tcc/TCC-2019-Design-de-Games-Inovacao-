using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObject : MonoBehaviour
{
    public Points coin;

    public int coinValue;

    public void PegouMoeda()
    {
        switch (coinValue)
        {
            case 1:
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Feedback/Coletaveis/Coin1");
                break;
            case 5:
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Feedback/Coletaveis/Coin5");
                break;
            case 10:
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Feedback/Coletaveis/Coin10");
                break;
            case 100:
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Feedback/Coletaveis/Coin100");
                break;
        }
        coin.Add(coinValue);
        Debug.Log("Pegou a moeda");
        Destroy(gameObject);
    }
}
