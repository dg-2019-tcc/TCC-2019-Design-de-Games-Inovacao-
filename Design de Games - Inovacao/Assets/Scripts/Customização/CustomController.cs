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
    private AnimDB animDB;
    private void Start()
    {
        lado.SetActive(true);
        frente.SetActive(true);

        takeOffFrente = frente.GetComponent<TakeOffUnused>();
        takeOffLado = lado.GetComponent<TakeOffUnused>();

        customDisplay = GetComponent<CustomDisplay>();
        animations = GetComponent<Player2DAnimations>();
        animDB = GetComponent<AnimDB>();

        GameManager.Instance.ChecaFase();
        customDisplay.DesativaTudo();
        //TiraCustomDesativada();
    }


    public void TiraCustomDesativada()
    {
        // customDisplay.AtivaTudo();
        //if (!GameManager.Instance.fase.Equals(GameManager.Fase.Loja))
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
        }

        if (takeOffFrente.letThemBeOn == true/* && takeOffLado.letThemBeOn == true*/)
        {
            animDB.ChangeArmature(0);
            animDB.CallAnimState04(AnimState04.Idle);
            //animations.PlayAnim(Player2DAnimations.State.Idle);
        }
    }
}
