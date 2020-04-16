using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Gerador/AI/Action/Futebol/GoUp")]
public class FutGoUpAction : Actions
{
    public override void Act(StateController controller)
    {
        GoUp(controller);
    }

    private void GoUp(StateController controller)
    {
        if (controller.canJump == true)
        {
            Debug.Log("Jump");

            controller.rb.AddForce(new Vector2(0, controller.botStats.jumpForce), ForceMode2D.Impulse);
        }
    }
}
