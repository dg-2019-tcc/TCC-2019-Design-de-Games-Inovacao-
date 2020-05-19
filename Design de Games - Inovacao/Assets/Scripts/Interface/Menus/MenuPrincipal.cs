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
    public BoolVariable acabou01;

    private void Start()
    {
        acabou01 = Resources.Load<BoolVariable>("Acabou01");
        flowIndex = Resources.Load<FloatVariable>("FlowIndex");
    }

    public void ComecaJogo()
    {
        if(acabou01.Value == true || flowIndex.Value >= 1)
        {
            nomeDoMenu = "HUB";
        }

        else
        {
            nomeDoMenu = "Historia";
        }
        Debug.Log(nomeDoMenu);
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Start", GetComponent<Transform>().position);
        SceneManager.LoadScene(nomeDoMenu);
    }

	public void Creditos()
	{
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        SceneManager.LoadScene(nomeDosCreditos);
	}
}
