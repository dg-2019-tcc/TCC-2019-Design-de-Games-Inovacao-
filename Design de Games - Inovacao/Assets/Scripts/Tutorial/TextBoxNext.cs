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

	private bool finish;

	private void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
		sprite.sprite = imagens[1];
		timeScaleBase = Time.timeScale;
		joystick = FindObjectOfType<Joystick>().gameObject;
		throwObject = FindObjectOfType<ThrowObject>();
		finish = false;
		//gameObject.SetActive(false);
	}


	private void Update()
	{
		if (finish || Input.GetKey(KeyCode.Space))
		{
			Time.timeScale = timeScaleBase;
			joystick.SetActive(true);
			Destroy(gameObject);

		}
		if (joystick == null)
		{
			joystick = FindObjectOfType<Joystick>().gameObject;
			throwObject = FindObjectOfType<ThrowObject>();
		}

		joystick.SetActive(false);
		Time.timeScale = 0;
		if (dogBotao.Value || throwObject.atirou)
		{
			

			dogBotao.Value = false;
			finish = true;
			Destroy(gameObject);
		}

		
	}


	private void OnDestroy()
	{
		Time.timeScale = timeScaleBase;
		joystick.SetActive(true);
	}



}
