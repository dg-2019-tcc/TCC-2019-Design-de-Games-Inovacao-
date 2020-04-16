using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gerador/AI/Decision/GoLeft")]
public class ColetaGoLeftDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetIsLeft = IsLeft(controller);
        return targetIsLeft;
    }

    private bool IsLeft(StateController controller)
    {
        float aiPos = Mathf.Abs(controller.transform.position.x);
        float coletavelPos = controller.wayPointList[controller.nextWayPoint].transform.position.x;

        float distance = aiPos + coletavelPos;

        Vector3 controllerPos = controller.transform.position;
        Vector3 coletaPos = controller.wayPointList[controller.nextWayPoint].transform.position;

        if (distance < 0 && coletaPos.y - controllerPos.y <= 1.7f)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
