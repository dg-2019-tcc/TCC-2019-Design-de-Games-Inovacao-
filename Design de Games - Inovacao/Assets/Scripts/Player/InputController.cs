using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [HideInInspector]
    public PhotonView PV;

    [SerializeField]
    public FloatingJoystick joyStick;
    public BoolVariable buildPC;

    [HideInInspector]
    public Vector2 joyInput;

    [HideInInspector]
    public bool releaseX;
    [HideInInspector]
    public bool pressX;

    #region Unity Function
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        buildPC = Resources.Load<BoolVariable>("BuildPC");

        if (buildPC.Value == false)
        {
            joyStick = FindObjectOfType<FloatingJoystick>();
        }
    }

    private void Update()
    {
        ShouldUpdate();
        if (buildPC.Value == false) { BuildMobile();}
        else{ BuildPC();}
    }
    #endregion

    #region Public Functions

    #endregion

    #region Private Functions
    void ShouldUpdate()
    {
        if (!PV.IsMine && GameManager.inRoom) return;
        if (GameManager.pausaJogo) return;
    }

    void BuildMobile()
    {
        if (joyStick == null)
        {
            joyStick = FindObjectOfType<FloatingJoystick>();
        }
        joyInput = new Vector2(joyStick.Horizontal, joyStick.Vertical);
    }

    void BuildPC()
    {
        joyInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.X))
        {
            pressX = true;
            releaseX = false;
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            pressX = false;
            releaseX = true;
        }
        else
        {
            pressX = false;
            releaseX = false;
        }
    }
    #endregion

}
