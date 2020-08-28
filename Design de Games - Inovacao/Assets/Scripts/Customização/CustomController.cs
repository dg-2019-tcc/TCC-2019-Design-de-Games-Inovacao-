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

    private Player2DAnimations animations;

    private void Start()
    {
        lado.SetActive(true);
        frente.SetActive(true);

        takeOffFrente = frente.GetComponent<TakeOffUnused>();
        takeOffLado = lado.GetComponent<TakeOffUnused>();

        customDisplay = GetComponent<CustomDisplay>();
        animations = GetComponent<Player2DAnimations>();

        GameManager.Instance.ChecaFase();
        customDisplay.DesativaTudo();
        //TiraCustomDesativada();
    }


    public void TiraCustomDesativada()
    {
        // customDisplay.AtivaTudo();
        Debug.Log("[Custom Controller] TiraCustomDesativa() sceneAtual =" + GameManager.Instance.sceneAtual);
        //if (!GameManager.Instance.fase.Equals(GameManager.Fase.Loja))
        if (GameManager.Instance.sceneAtual == SceneType.Cabelo || GameManager.Instance.sceneAtual == SceneType.Shirt || GameManager.Instance.sceneAtual == SceneType.Tenis || GameManager.Instance.sceneAtual == SceneType.Customiza)
        {
            takeOffFrente.letThemBeOn = true;
            takeOffLado.letThemBeOn = true;
            lado.SetActive(false);
            Debug.Log("[Custom Controller] Desativa player de lado");
        }
        else
        {
            takeOffFrente.CheckAndExecute();
            takeOffLado.CheckAndExecute();
        }

        if (takeOffFrente.letThemBeOn == true/* && takeOffLado.letThemBeOn == true*/)
        {
            animations.PlayAnim(Player2DAnimations.State.Idle);
        }
        Debug.Log("[Custom Controller] TiraCustomDesativada() Acabou");
    }
}
