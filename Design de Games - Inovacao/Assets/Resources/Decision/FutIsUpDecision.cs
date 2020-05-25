using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gerador/AI/Decision/FutIsUp")]
public class FutIsUpDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool ballIsUp = BallUp(controller);
        return ballIsUp;
    }

    private bool BallUp(StateController controller)
    {
        float aiPosY = Mathf.Abs(controller.transform.position.y);
        float bolaPosY = Mathf.Abs(controller.wayPointList[0].transform.position.y);

        float distanceY = aiPosY - bolaPosY;

        float aiPosX = Mathf.Abs(controller.transform.position.x);
        float bolaPosX = controller.wayPointList[0].transform.position.x;
        //float distance = AngleDir(controllerPos, bolaPos);

        float distanceX = aiPosX + bolaPosX;
        //Debug.Log(distanceX);

        if (distanceY < -1.5f && distanceX > -1f && distanceX < 1f && controller.canJump == true)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
