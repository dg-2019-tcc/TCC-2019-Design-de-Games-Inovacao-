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
        float aiPosY = Mathf.Abs(controller.transform.position.y);
        float bolaPosY = Mathf.Abs(controller.wayPointList[controller.nextWayPoint].transform.position.y);

        float distanceY = aiPosY - bolaPosY;

        float aiPosX = Mathf.Abs(controller.transform.position.x);
        float bolaPosX = Mathf.Abs(controller.wayPointList[controller.nextWayPoint].transform.position.x);

        float distanceX = aiPosX - bolaPosX;

        if (distanceY < 1.5f)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
