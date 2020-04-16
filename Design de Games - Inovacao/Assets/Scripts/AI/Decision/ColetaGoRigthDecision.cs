using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gerador/AI/Decision/GoRight")]
public class ColetaGoRigthDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetIsRight = IsRight(controller);
        return targetIsRight;
    }

    private bool IsRight(StateController controller)
    {
        float aiPos = Mathf.Abs(controller.transform.position.x);
        float coletavelPos = controller.wayPointList[controller.nextWayPoint].transform.position.x;

        float distance = aiPos + coletavelPos;

        Debug.Log(controller.nextWayPoint);

        Vector3 controllerPos = controller.transform.position;
        Vector3 coletaPos = controller.wayPointList[controller.nextWayPoint].transform.position;

        if (distance > 0 && coletaPos.y - controllerPos.y <= 1.7f)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
