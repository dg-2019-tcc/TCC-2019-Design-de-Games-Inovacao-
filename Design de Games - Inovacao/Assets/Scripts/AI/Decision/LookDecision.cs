using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Gerador/AI/Decision/Look")]
public class LookDecision : Decision
{
   public override bool Decide(StateController controller)
    {
        bool targetNotVisible = Look(controller);
        return targetNotVisible;
    }

    private bool Look(StateController controller)
    {
        Vector3 controllerPos = controller.transform.position;
        Vector3 coletaPos = controller.wayPointList[controller.nextWayPoint].transform.position;

        if(coletaPos.y - controllerPos.y <= 2)
        {
            return false;
        }

        else
        {
            return true;
        }
    }
}
