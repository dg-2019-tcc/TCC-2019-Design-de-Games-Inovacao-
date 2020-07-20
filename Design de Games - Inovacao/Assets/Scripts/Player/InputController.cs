using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    public FloatingJoystick joyStick;
    public BoolVariable buildPC;

    [HideInInspector]
    public Vector2 joyInput;

    [HideInInspector]
    public bool pressX;

    private void Awake()
    {
        buildPC = Resources.Load<BoolVariable>("BuildPC");

        if(buildPC.Value == false)
        {
            joyStick = FindObjectOfType<FloatingJoystick>();
        }
    }

    private void Update()
    {
        if (buildPC.Value == false)
        {
            if (joyStick == null)
            {
                joyStick = FindObjectOfType<FloatingJoystick>();
            }

            joyInput = new Vector2(joyStick.Horizontal, joyStick.Vertical);
        }

        else
        {
            joyInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (Input.GetKeyDown(KeyCode.X))
            {
                pressX = true;
            }

            if (Input.GetKeyUp(KeyCode.X))
            {
                pressX = false;
            }
        }
    }
}
