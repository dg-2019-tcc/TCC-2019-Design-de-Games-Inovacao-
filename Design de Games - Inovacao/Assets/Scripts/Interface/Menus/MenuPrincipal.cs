using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField]
    private string nomeDoMenu;
	[SerializeField]
	private string nomeDosCreditos;

    public FloatVariable flowIndex;
    public BoolVariableArray acabou01;

    private void Start()
    {
        acabou01 = Resources.Load<BoolVariableArray>("Acabou01");
        flowIndex = Resources.Load<FloatVariable>("FlowIndex");
    }

    public void ComecaJogo()
    {
        if(acabou01.Value[0] == true)
        {
            nomeDoMenu = "HUB";
        }

        else
        {
            nomeDoMenu = "Historia";
        }
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Start", GetComponent<Transform>().position);
        SceneManager.LoadScene(nomeDoMenu);
    }

	public void Creditos()
	{
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        SceneManager.LoadScene(nomeDosCreditos);
	}
}
