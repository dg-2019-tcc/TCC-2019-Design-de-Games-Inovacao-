using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityCore.Scene;

public class GoToMenu : MonoBehaviour
{
    void Start()
    {
		StartCoroutine(WaitAndGo());
    }

	private IEnumerator WaitAndGo()
	{
		yield return new WaitForSeconds(0.5f);
        //SceneManager.LoadScene(2);
       LoadingManager.instance.LoadNewScene(SceneType.MenuPrincipal, SceneType.FirstScene, false);
    }
    
}
