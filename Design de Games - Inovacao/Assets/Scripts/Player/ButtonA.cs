using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonA : MonoBehaviour
{
    public enum State {Atirar, Fala, PowerUp, Chutar, Cortar, Manobra}
    public State state = State.Fala;

    private DogController dogScript;
    private ThrowObject tiroScript;
    private FutebolPlayer chuteScript;
    private HandVolei corteScript;
    private EmpinaMoto manobraScript;

    public BoolVariable textoAtivo;
    public BoolVariable desativaPower;
    public BoolVariable buildPC;

    public bool passouTexto;

    void Start()
    {
        textoAtivo = Resources.Load<BoolVariable>("TextoAtivo");
        desativaPower = Resources.Load<BoolVariable>("DesativaPower");
        buildPC = Resources.Load<BoolVariable>("BuildPC");
        dogScript = GetComponent<DogController>();
    }

    void Update()
    {
        if(textoAtivo.Value == true)
        {
            state = State.Fala;
        }

        if (buildPC.Value)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                PressedButtonA();
            }
        }
    }

    public void PressedButtonA()
    {
        switch (state)
        {
            case State.Fala:
                PassarFala();
                break;

            case State.PowerUp:
                DesativaPowerUp();
                break;

            case State.Atirar:
                Atirar();
                break;

            case State.Chutar:
                Kick();
                break;

            case State.Cortar:
                CortarVolei();
                break;

            case State.Manobra:
                EmpinarMoto();
                break;
        }
    }

    public void PassarFala()
    {
        passouTexto = true;
    }

    public void DesativaPowerUp()
    {
        desativaPower.Value = true;
        state = State.Atirar;
    }

    public void Atirar()
    {
        if(tiroScript == null)
        {
            tiroScript = GetComponent<ThrowObject>();
        }

        tiroScript.Atirou();
    }

    public void Kick()
    {
        if(chuteScript == null)
        {
            chuteScript = GetComponent<FutebolPlayer>();
        }
        chuteScript.Chute();
    }

    public void CortarVolei()
    {
        if(corteScript == null)
        {
            corteScript = GetComponent<HandVolei>();
        }

        corteScript.Corte();
    }

    public void EmpinarMoto()
    {
        if(manobraScript == null)
        {
            manobraScript = FindObjectOfType<EmpinaMoto>();
        }

        manobraScript.buttonEmpina();
    }
}
