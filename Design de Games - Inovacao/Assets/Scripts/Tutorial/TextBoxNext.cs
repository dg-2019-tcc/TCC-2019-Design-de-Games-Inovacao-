using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBoxNext : MonoBehaviour
{
	public ScriptableFalas falas;
	private SpriteRenderer sprite;
	private GameObject joystick;
	private float timeScaleBase;
	private ButtonA buttonA;

	public TMP_Text text;

	public int boxIndex;
	private bool finish;

    public BoolVariable textoAtivo;

    public BoolVariableArray acabou01;

    public TV tv;

    public bool is00;

    public int acabou01Index;

    public int checkpoint;

	private void Start()
	{
        textoAtivo = Resources.Load<BoolVariable>("TextoAtivo");

        if (acabou01 == null)
        {
            acabou01 = Resources.Load<BoolVariableArray>("Acabou01");
        }


        sprite = GetComponent<SpriteRenderer>();
		boxIndex = 0;
		sprite.sprite = falas.Imagem();
		text.text = falas.falas[0];
		//timeScaleBase = Time.timeScale;
		//joystick = FindObjectOfType<Joystick>().gameObject;
		buttonA = FindObjectOfType<ButtonA>();
		finish = false;
		//StartCoroutine(Fade(1));

		//sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);

		//gameObject.SetActive(false);
	}


	private void Update()
	{
		if(finish)
		{
            textoAtivo.Value = false;
			Destroy(gameObject);

		}

		if (buttonA == null)
		{
			buttonA = FindObjectOfType<ButtonA>();
		}
		else if (buttonA.passouTexto)
		{
			Next(boxIndex);
		}
	}

	private void Next(int index)
	{
		if (index < falas.falas.Length-1)
		{
			boxIndex++;
			sprite.sprite = falas.Imagem();
			text.text = falas.falas[index];
            buttonA.passouTexto = false;


        }
		else
		{
            buttonA.passouTexto = false;
            textoAtivo.Value = false;
            finish = true;
            if (tv != null && !is00)
            {
                //acabou01.Value[acabou01Index] = true;
                //GameManager.Instance.SaveGame(GameManager.levelIndex, acabou01Index);
                //PlayerPrefsManager.Instance.SavePlayerPrefs("FalasIndex", acabou01Index);
                CheckPointController.instance.TalkCheckPoint(checkpoint);
                Debug.Log("Falou");
                //tv.FalouComTV();
            }
            Destroy(gameObject);
		}
	}


	




	void OnDestroy()
	{
        CheckPointController.instance.TalkCheckPoint(checkpoint);
        Debug.Log("Falou");
        textoAtivo.Value = false;
        //Time.timeScale = timeScaleBase;
        //joystick.SetActive(true);
	}

}
