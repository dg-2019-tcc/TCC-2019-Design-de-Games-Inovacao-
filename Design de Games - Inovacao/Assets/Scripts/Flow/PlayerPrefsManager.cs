using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public PlayerPrefsVariables prefsVariables;

    #region Singleton
    private static PlayerPrefsManager _instance;

    public static PlayerPrefsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerPrefsManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(PlayerPrefsManager).Name;
                    _instance = go.AddComponent<PlayerPrefsManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion


    public struct PlayerPrefsVariables
    {

        public int levelIndex;
        public int falasIndex;
        public int ganhouDoKlay;
        public int sequestrado;
        public int isFirstTime;
        public int hairIndex;
        public int shirtIndex;
        public int legsIndex;
        public int shoeIndex;
        public int oculosIndex;
        public int ciliosIndex;
        public int maskIndex;
        public int boneIndex;
        public int skinIndex;
        public int pupilaIndex;
        public int sombrancelhaIndex;

        /*public void ResetPlayerPrefs()
        {
            levelIndex = ganhouDoKlay = sequestrado = falasIndex = 0;
            hairIndex = shirtIndex = legsIndex = shoeIndex = oculosIndex = maskIndex = skinIndex = pupilaIndex = sombrancelhaIndex = 0;
            boneIndex = 10;

            PlayerPrefs.SetInt("LevelIndex", 0);
            PlayerPrefs.SetInt("FalasIndex", 0);
            PlayerPrefs.SetInt("GanhouDoKlay", 0);
            PlayerPrefs.SetInt("Sequestrado", 0);
            PlayerPrefs.SetInt("HairIndex", 0);
            PlayerPrefs.SetInt("LegsIndex", 0);
            PlayerPrefs.SetInt("ShoeIndex", 0);
            PlayerPrefs.SetInt("OculosIndex", 0);
            PlayerPrefs.SetInt("CiliosIndex", 0);
            PlayerPrefs.SetInt("MaskIndex", 0);
            PlayerPrefs.SetInt("BoneIndex", 10);
            PlayerPrefs.SetInt("SkinIndex", 0);
            PlayerPrefs.SetInt("PupilaIndex", 0);
            PlayerPrefs.SetInt("SombrancelhaIndex", 0);

            Debug.Log("Reseta as Player Prefs");
        }*/
    }



    public void LoadPlayerPref(string prefsName)
    {
        switch (prefsName)
        {
            case "LevelIndex":
                prefsVariables.levelIndex = PlayerPrefs.GetInt("LevelIndex");
                break;

            case "FalasIndex":
                prefsVariables.falasIndex = PlayerPrefs.GetInt("FalasIndex");
                break;

            case "GanhouDoKlay":
                prefsVariables.ganhouDoKlay = PlayerPrefs.GetInt("GanhouDoKlay");
                break;

            case "Sequestrado":
                prefsVariables.sequestrado = PlayerPrefs.GetInt("Sequestrado");
                break;

            case "Customização":
                prefsVariables.hairIndex = PlayerPrefs.GetInt("HairIndex");
                prefsVariables.shirtIndex = PlayerPrefs.GetInt("ShirtIndex");
                prefsVariables.legsIndex = PlayerPrefs.GetInt("LegsIndex");
                prefsVariables.shoeIndex = PlayerPrefs.GetInt("ShoeIndex");
                prefsVariables.oculosIndex = PlayerPrefs.GetInt("OculosIndex");
                prefsVariables.ciliosIndex = PlayerPrefs.GetInt("CiliosIndex");
                prefsVariables.maskIndex = PlayerPrefs.GetInt("MaskIndex");
                prefsVariables.boneIndex = PlayerPrefs.GetInt("BoneIndex");
                prefsVariables.skinIndex = PlayerPrefs.GetInt("SkinIndex");
                prefsVariables.pupilaIndex = PlayerPrefs.GetInt("PupilaIndex");
                prefsVariables.sombrancelhaIndex = PlayerPrefs.GetInt("SombrancelhaIndex");
                break;

            case "All":
                prefsVariables.levelIndex = PlayerPrefs.GetInt("LevelIndex");
                prefsVariables.falasIndex = PlayerPrefs.GetInt("FalasIndex");
                prefsVariables.isFirstTime = PlayerPrefs.GetInt("IsFirstTime");
                prefsVariables.ganhouDoKlay = PlayerPrefs.GetInt("GanhouDoKlay");
                prefsVariables.sequestrado = PlayerPrefs.GetInt("Sequestrado");
                prefsVariables.hairIndex = PlayerPrefs.GetInt("HairIndex");
                prefsVariables.shirtIndex = PlayerPrefs.GetInt("ShirtIndex");
                prefsVariables.legsIndex = PlayerPrefs.GetInt("LegsIndex");
                prefsVariables.shoeIndex = PlayerPrefs.GetInt("ShoeIndex");
                prefsVariables.oculosIndex = PlayerPrefs.GetInt("OculosIndex");
                prefsVariables.ciliosIndex = PlayerPrefs.GetInt("CiliosIndex");
                prefsVariables.maskIndex = PlayerPrefs.GetInt("MaskIndex");
                prefsVariables.boneIndex = PlayerPrefs.GetInt("BoneIndex");
                prefsVariables.skinIndex = PlayerPrefs.GetInt("SkinIndex");
                prefsVariables.pupilaIndex = PlayerPrefs.GetInt("PupilaIndex");
                prefsVariables.sombrancelhaIndex = PlayerPrefs.GetInt("SombrancelhaIndex");
                break;
        }
    }


    public void SavePlayerPrefs(string prefsName, int prefsValue)
    {
        PlayerPrefs.SetInt(prefsName, prefsValue);
        if (prefsName == "LevelIndex" || prefsName == "FalasIndex")
        {
            Debug.Log(prefsName + " " + prefsValue);
        }
        switch (prefsName)
        {
            case "LevelIndex":
                prefsVariables.levelIndex = prefsValue;
                break;

            case "FalasIndex":
                prefsVariables.falasIndex = prefsValue;
                break;

            case "GanhouDoKlay":
                prefsVariables.ganhouDoKlay = prefsValue;
                break;

            case "Sequestrado":
                prefsVariables.sequestrado = prefsValue;
                break;

            case "IsFirstTime":
                prefsVariables.isFirstTime = prefsValue;
                break;

            case "HairIndex":
                prefsVariables.hairIndex = prefsValue;
                break;

            case "ShirtIndex":
                prefsVariables.shirtIndex = prefsValue;
                break;

            case "LegsIndex":
                prefsVariables.legsIndex = prefsValue;
                break;

            case "ShoeIndex":
                prefsVariables.shoeIndex = prefsValue;
                break;

            case "OculosIndex":
                prefsVariables.oculosIndex = prefsValue;
                break;

            case "CiliosIndex":
                prefsVariables.ciliosIndex = prefsValue;
                break;

            case "MaskIndex":
                prefsVariables.maskIndex = prefsValue;
                break;

            case "BoneIndex":
                prefsVariables.boneIndex = prefsValue;
                break;

            case "SkinIndex":
                prefsVariables.skinIndex = prefsValue;
                break;

            case "PupilaIndex":
                prefsVariables.pupilaIndex = prefsValue;
                break;

            case "SombrancelhaIndex":
                prefsVariables.sombrancelhaIndex = prefsValue;
                break;
        }
    }

}
