using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gerador/AI/Action/ColetaLeft")]
public class ColetaLeftAction : Actions
{
    public override void Act(StateController controller)
    {
        GoRight(controller);
    }

    private void GoRight(StateController controller)
    {
        controller.rb.velocity = new Vector2(-controller.botStats.moveSpeed, 0);
    }
}
