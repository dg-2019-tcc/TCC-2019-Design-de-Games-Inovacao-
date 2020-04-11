using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Gerador/AI/Decision/Look")]
public class LookDecision : Decision
{
   public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    private bool Look(StateController controller)
    {
        if(controller.wayPointList[controller.nextWayPoint].transform.position.y - controller.pos.transform.position.y >= 2)
        {
            return false;
        }

        else
        {
            return true;
        }
    }
}
