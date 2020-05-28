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

	private void Start()
	{

		sprite = GetComponent<SpriteRenderer>();
		boxIndex = 0;
		sprite.sprite = imagens[boxIndex];
		timeScaleBase = Time.timeScale;
		joystick = FindObjectOfType<Joystick>().gameObject;
		throwObject = FindObjectOfType<ThrowObject>();
		finish = false;
		StartCoroutine(Fade(1));

		sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);

		//gameObject.SetActive(false);
	}


	private void Update()
	{
		switch (fadeAnimIndex)
		{
			case 1:
				sprite.color = Color.Lerp(sprite.color, new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1), Time.deltaTime*3);
				joystick.SetActive(false);
				break;

			case 2:
				sprite.color = Color.Lerp(sprite.color, new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0), Time.deltaTime*3);
				break;
			case 0:

				if (finish || Input.GetKey(KeyCode.Space))
				{
				//	Time.timeScale = timeScaleBase;
					joystick.SetActive(true);
					Destroy(gameObject);

				}
				if (joystick == null)
				{
					joystick = FindObjectOfType<Joystick>().gameObject;
					throwObject = FindObjectOfType<ThrowObject>();
				}

				
			//	Time.timeScale = 0;

				if (dogBotao.Value || throwObject.atirou)
				{
					Debug.Log("apertou o botão");
					Next(boxIndex);
				}
				break;
		}
	}

	private void Next(int index)
	{
		if (index < imagens.Length-1)
		{
			boxIndex++;
			sprite.sprite = imagens[boxIndex];
		//	dogBotao.Value = true;

		}
		else
		{
		//	dogBotao.Value = false;
			finish = true;
			StartCoroutine(Fade(2));
		}
	}


	




	private int fadeAnimIndex;
	private IEnumerator Fade(int inOrOut)
	{
		fadeAnimIndex = inOrOut;
		Time.timeScale = timeScaleBase;
		joystick.SetActive(true);
		yield return new WaitForSeconds(1);
		fadeAnimIndex = 0;
		if (inOrOut == 2)
		{
			
			Destroy(gameObject);
		}
	}

}
