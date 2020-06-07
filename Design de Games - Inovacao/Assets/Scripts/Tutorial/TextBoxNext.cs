using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxNext : MonoBehaviour
{
	public Sprite[] imagens;
	private SpriteRenderer sprite;
	public BoolVariable dogBotao;
	private GameObject joystick;
	private float timeScaleBase;
	private ThrowObject throwObject;

	public int boxIndex;
	private bool finish;

    public BoolVariable textoAtivo;

    public BoolVariableArray acabou01;

    public TV tv;

    public bool is00;

    public int acabou01Index;

	private void Start()
	{
        textoAtivo = Resources.Load<BoolVariable>("TextoAtivo");

        if (acabou01 == null)
        {
            acabou01 = Resources.Load<BoolVariableArray>("Acabou01");
        }


        sprite = GetComponent<SpriteRenderer>();
		boxIndex = 0;
		sprite.sprite = imagens[boxIndex];
		//timeScaleBase = Time.timeScale;
		joystick = FindObjectOfType<Joystick>().gameObject;
		throwObject = FindObjectOfType<ThrowObject>();
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

		if (throwObject == null)
		{
			throwObject = FindObjectOfType<ThrowObject>();
		}
		else if (throwObject.passouTexto)
		{
			Next(boxIndex);
		}
	}

	private void Next(int index)
	{
		if (index < imagens.Length-1)
		{
			boxIndex++;
			sprite.sprite = imagens[boxIndex];
            throwObject.passouTexto = false;


        }
		else
		{
            throwObject.passouTexto = false;
            textoAtivo.Value = false;
            finish = true;
            if (tv != null && !is00)
            {
                acabou01.Value[acabou01Index] = true;
                tv.FalouComTV();
            }
            Destroy(gameObject);
		}
	}


	




	void OnDestroy()
	{
        textoAtivo.Value = false;
        //Time.timeScale = timeScaleBase;
        joystick.SetActive(true);
	}

}
