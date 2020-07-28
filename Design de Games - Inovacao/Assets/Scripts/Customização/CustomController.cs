using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        customDisplay.AtivaRoupas();
    }

    public void TiraCustomDesativada()
    {
        if (!GameManager.Instance.fase.Equals(GameManager.Fase.Loja))
        {
            takeOffFrente.CheckAndExecute();
            takeOffLado.CheckAndExecute();

            Debug.Log("Não é Loja");
        }
        else
        {
            takeOffFrente.letThemBeOn = true;
            takeOffLado.letThemBeOn = true;
            lado.SetActive(false);
            Debug.Log("Loja");
        }

        if (takeOffFrente.letThemBeOn == true && takeOffLado.letThemBeOn == true)
        {
            animations.PlayAnim(Player2DAnimations.State.Idle);
            Debug.Log("Chama o Idle");
        }
        Debug.Log("Tira Customização Desativada");
    }
}
