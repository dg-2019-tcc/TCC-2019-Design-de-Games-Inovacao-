using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AI/Decision/GoRight")]
public class ColetaGoRigthDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetIsRight = IsRight(controller);
        return targetIsRight;
    }

    private bool IsRight(StateController controller)
    {
        Vector2 aiPos = controller.transform.position;
        Vector2 coletavelPos = controller.wayPointList[controller.nextWayPoint].transform.position;

        Vector3 controllerPos = controller.transform.position;
        Vector3 coletaPos = controller.wayPointList[controller.nextWayPoint].transform.position;

        if (aiPos.x - coletaPos.x < 0)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
