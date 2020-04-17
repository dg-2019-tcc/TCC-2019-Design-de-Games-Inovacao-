using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gerador/AI/Action/Stop")]
public class StopAction : Actions
{
    public override void Act(StateController controller)
    {
        Stop(controller);
    }

    private void Stop(StateController controller)
    {
        controller.rb.velocity = new Vector2(0, 0);
    }
}
