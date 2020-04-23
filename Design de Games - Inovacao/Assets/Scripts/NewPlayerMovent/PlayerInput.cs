using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Player2D))]
public class PlayerInput : MonoBehaviour
{
    /*Player2D player2D;

    [SerializeField]
    public Joystick joyStick;

    void Start()
    {
        player2D = GetComponent<Player2D>();
        joyStick = FindObjectOfType<Joystick>();
    }

    void FixedUpdate()
    {
        Vector2 directionalInput = new Vector2(joyStick.Horizontal, joyStick.Vertical);
        player2D.SetDirectionalInput(directionalInput);
    }*/
}
