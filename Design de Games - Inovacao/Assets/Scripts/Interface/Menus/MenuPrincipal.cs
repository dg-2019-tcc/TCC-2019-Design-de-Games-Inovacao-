using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityCore.Scene;
using UnityCore.Menu;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField]
    private string nomeDoMenu;
	[SerializeField]
	private string nomeDosCreditos;
    private SceneType nextScene;

    #region Unity Function

    private void Start()
    {
        GameManager.sceneAtual = SceneType.MenuPrincipal;
        GameManager.Instance.ChecaFase();
    }

    #endregion

    #region Public Functions

    public void ComecaJogo()
    {
        if (CheckPointController.checkPointIndex > 0 || GameManager.historiaMode == false)
        {
			Debug.Log("checkpoint is " + CheckPointController.checkPointIndex);

			nomeDoMenu = "HUB";
            nextScene = SceneType.HUB;
            //Debug.Log("ComecaJOGO[MenuPrincipal script]");
        }

        else
        {
            nomeDoMenu = "Historia";
            nextScene = SceneType.Historia;
        }
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Start", GetComponent<Transform>().position);
        LoadingManager.instance.LoadNewScene(nextScene, SceneType.MenuPrincipal, false);
        /*SceneController.Instance.Load(nextScene, (_scene) => {
            Debug.Log("Scene [" + _scene + "] loaded from MenuPrincipal scrípt!");
        }, false, PageType.Loading);*/
        //SceneManager.LoadScene(nomeDoMenu);
    }

    public void Creditos()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        SceneController.Instance.Load(SceneType.Creditos, (_scene) => {
            Debug.Log("Scene [" + _scene + "] loaded from MenuPrincipal scrípt!");
        }, false, PageType.Loading);
        //SceneManager.LoadScene(nomeDosCreditos);
    }

    #endregion

    #region Private Functions

    #endregion
}
