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

	private void Start()
	{
        textoAtivo = Resources.Load<BoolVariable>("TextoAtivo");

        sprite = GetComponent<SpriteRenderer>();
		boxIndex = 0;
		sprite.sprite = imagens[boxIndex];
		//timeScaleBase = Time.timeScale;
		joystick = FindObjectOfType<Joystick>().gameObject;
		throwObject = FindObjectOfType<ThrowObject>();
		finish = false;
		
		//gameObject.SetActive(false);
	}


	private void Update()
	{
		if (finish || Input.GetKey(KeyCode.Space))
		{
            textoAtivo.Value = false;
			Destroy(gameObject);

		}


		if (throwObject.passouTexto)
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
			Destroy(gameObject);
		}
	}


	private void OnDestroy()
	{
        textoAtivo.Value = false;
        //Time.timeScale = timeScaleBase;
        joystick.SetActive(true);
	}



}
