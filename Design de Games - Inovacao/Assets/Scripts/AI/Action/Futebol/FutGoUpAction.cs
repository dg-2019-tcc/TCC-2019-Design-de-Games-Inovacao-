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
        if (controller.canJump == true && controller.jumpCooldown > 2f)
        {
            Debug.Log(controller.canJump);

            controller.rb.AddForce(new Vector2(controller.rb.velocity.x, controller.botStats.jumpForce ), ForceMode2D.Impulse);
        }


    }
}
