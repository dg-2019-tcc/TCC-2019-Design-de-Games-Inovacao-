using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AI/Decision/GoLeft")]
public class ColetaGoLeftDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetIsLeft = IsLeft(controller);
        return targetIsLeft;
    }

    private bool IsLeft(StateController controller)
    {
        Vector2 aiPos = controller.transform.position;
        Vector2 coletavelPos = controller.wayPointList[controller.nextWayPoint].transform.position;


        Vector3 controllerPos = controller.transform.position;
        Vector3 coletaPos = controller.wayPointList[controller.nextWayPoint].transform.position;

        if (aiPos.x - coletaPos.x > 0 && coletavelPos.y - aiPos.y < 1.7f)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
