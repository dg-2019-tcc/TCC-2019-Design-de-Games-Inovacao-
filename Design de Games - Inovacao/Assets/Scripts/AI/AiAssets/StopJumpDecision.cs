using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gerador/AI/Decision/StopJump")]
public class StopJumpDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetDown = StopJump(controller);
        return targetDown;
    }

    private bool StopJump(StateController controller)
    {
        Vector3 controllerPos = controller.transform.position;
        Vector3 coletaPos = controller.wayPointList[controller.nextWayPoint].transform.position;

        if(controllerPos.y - coletaPos.y < 0)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
