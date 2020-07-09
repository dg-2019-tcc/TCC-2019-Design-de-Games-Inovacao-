using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
	public BoolVariable buildPC;

    void Start()
    {
		SetVariables();
		CheckWhichBuild();
	}

	private void SetVariables()
	{
		buildPC = Resources.Load<BoolVariable>("BuildPC");
	}

	private void CheckWhichBuild()
	{
#if UNITY_EDITOR
		Debug.Log("Unity Editor");
		buildPC.Value = true;
#elif UNITY_STANDALONE_WIN
		Debug.Log("Stand Alone Windows Build");
		buildPC.Value = true;
#elif UNITY_ANDROID
		Debug.Log("Android Build");
		buildPC.Value = false;
#else
		Debug.Log("Unpredicted Platform, defaulting to android build");
		buildPC.Value = false;
#endif
	}

}
