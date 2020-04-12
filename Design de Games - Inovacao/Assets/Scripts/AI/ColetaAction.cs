using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gerador/AI/Action/Coleta")]
public class ColetaAction : Actions
{
    public override void Act(StateController controller)
    {
        Coleta(controller);
    }

    private void Coleta(StateController controller)
    {
        Vector3 controllerPos = controller.transform.position;
        Vector3 coletaPos = controller.wayPointList[controller.nextWayPoint].transform.position;

        if(controllerPos.x - coletaPos.x <= -1)
        {
            controller.rb.velocity = new Vector3(controller.enemyStats.moveSpeed, 0, 0);
        }

        else if (controllerPos.x - coletaPos.x >= 1)
        {
            controller.rb.velocity = new Vector3(-controller.enemyStats.moveSpeed, 0, 0);
        }

    }
}

