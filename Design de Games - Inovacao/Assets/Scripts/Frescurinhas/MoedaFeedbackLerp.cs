using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoedaFeedbackLerp : MonoBehaviour
{
    public static MoedaFeedbackLerp instance;
    public DOTweenUI tweenUI;
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
    public void MoedaCanvasIsActive(bool on)
    {
        showingCoin = on;
        if (on == true)
        {
            timer -= 3;
            //objUI.SetActive(true);
            ShowMoedas();
            tweenUI.TweenIn();
        }
        else { tweenUI.TweenOut(); }
    }

    void ShowMoedas()
    {
        pontosDisplay = Mathf.Lerp(pontosDisplay, moedas.Value, 1f);
        texto.text = pontosDisplay.ToString();
        while(timer < timeToDisable) { return; }
    }

    
}
