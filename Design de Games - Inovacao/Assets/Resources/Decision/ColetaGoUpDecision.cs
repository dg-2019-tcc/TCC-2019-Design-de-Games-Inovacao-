using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gerador/AI/Decision/GoUp")]
public class ColetaGoUpDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetIsUp = IsUp(controller);
        return targetIsUp;
    }

    private bool IsUp(StateController controller)
    {
        Vector2 controllerPos = controller.transform.position;
        Vector2 coletaPos = controller.wayPointList[controller.nextWayPoint].transform.position;

        if (coletaPos.y - controllerPos.y >= 1.7f && controller.canJump == true)
        {
            Debug.Log(controller.canJump);
            return true;
        }

        else
        {
            return false;
        }
    }

}
