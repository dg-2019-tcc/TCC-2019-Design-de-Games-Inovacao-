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
        controller.target = controller.wayPointList[controller.nextWayPoint].transform;

        float step = controller.enemyStats.moveSpeed * Time.deltaTime;

        controller.transform.position = Vector3.MoveTowards(controller.transform.position, controller.target.position, step);

        if (Vector3.Distance(controller.transform.position, controller.target.position) < 1f)
        {
            controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
        }
    }

}
