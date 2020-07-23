using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

public class ButtonA : MonoBehaviour
{
    public enum State {Atirar, Fala, PowerUp, Chutar, Cortar, Manobra, Null}
    public State state = State.Null;

    private DogController dogScript;
    private ThrowObject tiroScript;
    private FutebolPlayer chuteScript;
    private HandVolei corteScript;
    private EmpinaMoto manobraScript;

    public BoolVariable textoAtivo;
    public BoolVariable desativaPower;
    public BoolVariable buildPC;
    public BoolVariable carroActive;
    public BoolVariable pipaActive;

    public bool passouTexto;

    void Start()
    {
        textoAtivo = Resources.Load<BoolVariable>("TextoAtivo");
        desativaPower = Resources.Load<BoolVariable>("DesativaPower");
        buildPC = Resources.Load<BoolVariable>("BuildPC");
        pipaActive = Resources.Load<BoolVariable>("PipaActive");
        carroActive = Resources.Load<BoolVariable>("CarroActive");
        dogScript = GetComponent<DogController>();
    }

    void Update()
    {
        if(textoAtivo.Value == true)
        {
            state = State.Fala;
        }

        else if(textoAtivo.Value == false &&(pipaActive.Value == true ||carroActive.Value == true))
        {
            state = State.PowerUp;
        }

        else
        {
            state = State.Null;
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

            default:
                state = State.Null;
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
        if (dogScript.state.Equals(DogController.State.Idle))
        {
            dogScript.ChangeState("TiroState");
            tiroScript.Atirou();
        }
        else { return; }
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
