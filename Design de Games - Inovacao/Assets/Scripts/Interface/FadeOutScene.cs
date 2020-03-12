using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeOutScene : MonoBehaviour
{
	private Image image;
    // Start is called before the first frame update
    void Start()
    {
		image = GetComponent<Image>();
		image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
	}


	private void OnEnable()
	{
		image = GetComponent<Image>();
		image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
	}

	// Update is called once per frame
	void Update()
    {
		image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + 0.1f);
	}
}
