 using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HistoriaManager : MonoBehaviour
{
    public Image histImage;
    public Sprite[] histSprites;

    public string nomeDoMenu;


    private void Awake()
    {
        StartCoroutine("StartHist");
    }

    public void SkipHist()
    {
        SceneManager.LoadScene(nomeDoMenu);
    }

    IEnumerator StartHist()
    {
        for (int i = 0; i < histSprites.Length; i++)
        {
            histImage.sprite = histSprites[i];
            yield return new WaitForSeconds(5f);
        }

        SceneManager.LoadScene(nomeDoMenu);
    }
}
