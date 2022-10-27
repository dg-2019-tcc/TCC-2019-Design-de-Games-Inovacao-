using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;
using UnityCore.Scene;

public class ButtonA : MonoBehaviour
{
    [HideInInspector]
    public PhotonView PV;

    public enum State {Atirar, Fala, PowerUp, Chutar, Cortar, Manobra, Null}
    public State state = State.Null;

    private DogController dogScript;
    public DogAnim dogAnim;
    private ThrowObject tiroScript;
    private FutebolPlayer chuteScript;
    private HandVolei corteScript;
    private EmpinaMoto manobraScript;

    public BoolVariable textoAtivo;
    public BoolVariable desativaPower;
    public BoolVariable buildPC;

    public bool passouTexto;

    #region Unity Function
    void Start()
    {
        PV = GetComponent<PhotonView>();
        textoAtivo = Resources.Load<BoolVariable>("TextoAtivo");
        desativaPower = Resources.Load<BoolVariable>("DesativaPower");
        buildPC = Resources.Load<BoolVariable>("BuildPC");
        dogScript = GetComponent<DogController>();

		switch (GameManager.sceneAtual)
		{
			case SceneType.Coleta:
			case SceneType.Corrida:
				state = State.Atirar;
				break;
			case SceneType.Futebol:
				state = State.Chutar;
				break;
			case SceneType.Moto:
				state = State.Manobra;
				break;
			case SceneType.Volei:
				state = State.Cortar;
				break;
			
			default:
				break;
		}
    }

    void Update()
    {
        if (!PV.IsMine && GameManager.inRoom) return;
		//if (GameManager.Instance.fase.Equals(GameManager.Fase.Hub) || GameManager.Instance.fase.Equals(GameManager.Fase.Tutorial))

		switch (GameManager.sceneAtual)
		{
			case SceneType.HUB:
			case SceneType.Tutorial2:
				if (textoAtivo.Value == true)
				{
					state = State.Fala;
				}
				break;

			case SceneType.Futebol:
				state = State.Chutar;
				break;
			case SceneType.Moto:
				state = State.Manobra;
				break;
			case SceneType.Volei:
				state = State.Cortar;
				break;

			default:
				if (textoAtivo.Value == false &&(dogScript.state == Complete.DogController.State.Carro || dogScript.state == Complete.DogController.State.Pipa))
				{
					state = State.PowerUp;
				}
				break;

		}

		//----só para build de PC
        if (buildPC.Value)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                PressedButtonA();
            }

            if (Input.GetKeyUp(KeyCode.Z))
            {
                if (state == State.Manobra)
                {
                    manobraScript.stopManobra();
                }
            }
        }
		//-----------------------
    }
    #endregion

    #region Public Functions
    public void PressedButtonA()
    {
        if (GameManager.pausaJogo == true) { return; }
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
        Debug.Log("PressedButtonA");
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
        if (tiroScript == null)
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
        if (chuteScript == null)
        {
            chuteScript = GetComponent<FutebolPlayer>();
        }
        if (chuteScript.kicked == false)
        {
            chuteScript.Chute();
        }
    }

    public void CortarVolei()
    {
        if (corteScript == null)
        {
            corteScript = GetComponent<HandVolei>();
        }

        corteScript.Corte();
    }

    public void EmpinarMoto()
    {
        if (manobraScript == null)
        {
            manobraScript = FindObjectOfType<EmpinaMoto>();
        }
        Debug.Log("EmpinarMoto");
        manobraScript.buttonEmpina();
    }
    #endregion

    #region Private Functions

    #endregion
}
