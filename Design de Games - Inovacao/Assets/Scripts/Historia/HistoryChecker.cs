using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HistoryChecker : MonoBehaviour
{
	public static string sceneName;

	private void Start()
	{
		sceneName = SceneManager.GetActiveScene().name;
	}
}
