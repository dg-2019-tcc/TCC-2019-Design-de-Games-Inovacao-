using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimInfo : MonoBehaviour
{
    public PlayerInfo playerInfo;

    public struct PlayerInfo
    {
        //State01
        public bool stun;
        public bool win;
        public bool lose;


        //State02
        public bool pipaOn;
        public bool carroOn;
        public bool carroJump;
        public Vector2 carroVelocity;

        //State03
        public bool buttonZAnim;

        //State04
        public Vector2 velocity;
        public Vector2 input;
        public bool isGrounded;
        public bool jump;
    }

    public void UpdateInfo04(Vector2 vel, Vector2 inp, bool isGro, bool j)
    {
        if (playerInfo.velocity != vel) { playerInfo.velocity = vel; }
        if(playerInfo.input != inp) { playerInfo.input = inp; }
        if(playerInfo.isGrounded!= isGro) { playerInfo.isGrounded = isGro; }
        if(playerInfo.jump != j) { playerInfo.jump = j; }
    }

    public struct AnimInfo
    {
        public AnimState01 anim01;
        public AnimState02 anim02;
        public AnimState03 anim03;
        public AnimState04 anim04;

        public AnimState04 oldAnim04;
    }
}
