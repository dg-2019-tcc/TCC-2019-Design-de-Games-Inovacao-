using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.Scene;

public class CustomController : MonoBehaviour
{
    public GameObject frente;
    public GameObject lado;

    public TakeOffUnused takeOffFrente;
    public TakeOffUnused takeOffLado;

    private CustomDisplay customDisplay;

    #region Unity Function
    private void Start()
    {
        lado.SetActive(true);
        frente.SetActive(true);

        takeOffFrente = frente.GetComponent<TakeOffUnused>();
        takeOffLado = lado.GetComponent<TakeOffUnused>();

        customDisplay = GetComponent<CustomDisplay>();

        GameManager.Instance.ChecaFase();
        customDisplay.DesativaTudo();
    }
    #endregion

    #region Public Functions

    #endregion

    #region Private Functions
    public void TiraCustomDesativada()
    {
        if (GameManager.sceneAtual == SceneType.Cabelo || GameManager.sceneAtual == SceneType.Shirt || GameManager.sceneAtual == SceneType.Tenis || GameManager.sceneAtual == SceneType.Customiza)
        {
            takeOffFrente.letThemBeOn = true;
            takeOffLado.letThemBeOn = true;
            lado.SetActive(false);
        }
        else
        {
            takeOffFrente.CheckAndExecute();
            takeOffLado.CheckAndExecute();
            if (GameManager.sceneAtual != SceneType.Moto) frente.SetActive(false);
        }
    }
    #endregion

}
