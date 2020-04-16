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

        Debug.Log(coletaPos.y - controllerPos.y);

        if(coletaPos.y - controllerPos.y >= 1.7f)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
