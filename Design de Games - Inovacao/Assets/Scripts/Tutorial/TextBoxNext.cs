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

	private bool finish;

	private void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
		sprite.sprite = imagens[1];
		timeScaleBase = Time.timeScale;
		joystick = FindObjectOfType<Joystick>().gameObject;
		finish = false;
		//gameObject.SetActive(false);
	}


	private void FixedUpdate()
	{
		if (finish)
		{
			Time.timeScale = 1;
			joystick.SetActive(true);
			Destroy(gameObject);

		}
		if (joystick == null)
		{
			joystick = FindObjectOfType<Joystick>().gameObject;
		}

		joystick.SetActive(false);
		Time.timeScale = 0;
		if (dogBotao.Value)
		{
			

			dogBotao.Value = false;
			finish = true;
			Destroy(gameObject);
		}

		
	}


	private void OnDestroy()
	{
		Time.timeScale = 1;
		joystick.SetActive(true);
	}



}
