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

    public void ComecaJogo()
    {
        SceneManager.LoadScene(nomeDoMenu);
    }

	public void Creditos()
	{
		SceneManager.LoadScene(nomeDosCreditos);
	}
}
