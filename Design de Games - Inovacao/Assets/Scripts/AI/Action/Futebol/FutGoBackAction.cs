using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gerador/AI/Action/Futebol/GoBack")]
public class FutGoBackAction : Actions
{
    public override void Act(StateController controller)
    {
        GoBack(controller);
    }

    private void GoBack(StateController controller)
    {
        controller.rb.velocity = new Vector2(controller.botStats.moveSpeed, controller.rb.velocity.y);
    }
}
