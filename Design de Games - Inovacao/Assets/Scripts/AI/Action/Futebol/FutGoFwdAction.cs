using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gerador/AI/Action/Futebol/GoFoward")]
public class FutGoFwdAction : Actions
{
    public override void Act(StateController controller)
    {
        GoFoward(controller);
    }

    private void GoFoward(StateController controller)
    {
        controller.rb.velocity = new Vector2(-controller.botStats.moveSpeed, 0);
    }
}
