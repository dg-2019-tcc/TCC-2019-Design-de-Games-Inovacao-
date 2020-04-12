using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gerador/AI/Action/Jump")]
public class JumpAction : Actions
{
    public override void Act(StateController controller)
    {
        Jump(controller);
    }

    private void Jump(StateController controller)
    {
        Debug.Log("Jump");

        controller.rb.AddForce(new Vector2(0, controller.enemyStats.jumpForce), ForceMode2D.Impulse);
    }

}
