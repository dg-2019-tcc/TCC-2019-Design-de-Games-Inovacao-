﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FMOD.Studio;
using FMODUnity;
public class MotoChangeSpeed : MonoBehaviour
{
    public float climbing = 0.15f;
    public float descendingSlope = 0.3f;
    public float descendingAir = 0.5f;
    public float boost01 = 0.5f;
    public float boost02 = 1f;
    public float boost03 = 2f;

    public FloatVariable motoSpeedChange;
    public BoolVariable levouDogada;
    public Controller2D controller;
    private NewMotoPlayerMovement player;
    public EmpinaMoto empina;
    public TextMeshProUGUI speedText;
    public float speedVal;

    [Header("Fmod")]
    public EventInstance CarEngine;
    float RPM;
    float AccelInput;

    #region Unity Function

    void Start()
    {
        player = GetComponent<NewMotoPlayerMovement>();
        controller = GetComponent<Controller2D>();

        CarEngine = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/MotoMotor");
        CarEngine.getParameterByName("RPM", out RPM);
        CarEngine.getParameterByName("Accel", out AccelInput);

        FMODUnity.RuntimeManager.AttachInstanceToGameObject(CarEngine, GetComponent<Transform>(), GetComponent<Rigidbody2D>());

        levouDogada.Value = false;
        motoSpeedChange.Value = 0f;
    }


    void Update()
    {
        UpdateSpeedUI();
        CheckNormalSpeed();
        CheckTrickSpeed();

        if (levouDogada.Value){ motoSpeedChange.Value = 0f;}

        BikeSound();
    }


    private void OnDestroy()
    {
        CarEngine.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    #endregion

    #region Public Functions

    #endregion

    #region Private Functions
    void UpdateSpeedUI()
    {
        if (/*controller.collisions.bateuObs ||*/ levouDogada.Value)
        {
            motoSpeedChange.Value = 0f;
            float speedZero = 0f;
            speedText.text = speedZero.ToString() + "Km/h";
        }
        else
        {
            speedVal = (motoSpeedChange.Value + 15) * 5;
            speedVal = Mathf.RoundToInt(speedVal);
            speedText.text = speedVal.ToString() + "Km/h";
        }
    }

    void CheckNormalSpeed()
    {
        if (controller.collisions.climbingSlope)
        {
            motoSpeedChange.Value -= climbing * Time.deltaTime;
        }

        if (controller.collisions.descendingSlope)
        {
            motoSpeedChange.Value += descendingSlope * Time.deltaTime;
        }

        if (player.velocity.y < 0 && controller.collisions.below == false)
        {
            motoSpeedChange.Value += descendingAir * Time.deltaTime;
        }
    }

    void CheckTrickSpeed()
    {
        if (empina.isEmpinando) { motoSpeedChange.Value += boost01 * Time.deltaTime; }
        else if (empina.isManobrandoNoAr) { motoSpeedChange.Value += boost02 * Time.deltaTime; }
    }

    void BikeSound()
    {
        RPM = Mathf.Lerp(RPM, motoSpeedChange.Value / 10, 0.1f);
        CarEngine.setParameterByName("RPM", RPM);
        CarEngine.setParameterByName("Accel", motoSpeedChange.Value);
    }
    #endregion
}