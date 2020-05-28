using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxActivate : MonoBehaviour
{


	public GameObject TextBox;
	public GameObject blocker;

    public BoolVariable textoAtivo;

    private void Start()
    {
        textoAtivo = Resources.Load<BoolVariable>("TextoAtivo");
    }


    public void PlayerHit()
	{
		if (blocker != null) return;
		TextBox.SetActive(true);
        textoAtivo.Value = true;
		Destroy(gameObject);

	}
}
