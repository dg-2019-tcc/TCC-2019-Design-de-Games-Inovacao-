using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gerador/AI/Decision/FutIsFwd")]
public class FutIsFowardDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool ballIsFwd = BallFoward(controller);
        return ballIsFwd;
    }

    private bool BallFoward(StateController controller)
    {
        /*Vector2 controllerPos = controller.transform.position;
        Vector2 bolaPos = controller.wayPointList[0].transform.position;*/

        float aiPos =Mathf.Abs(controller.transform.position.x);
        float bolaPos = controller.wayPointList[0].transform.position.x;
        //float distance = AngleDir(controllerPos, bolaPos);

        float distance = aiPos + bolaPos;

        if (distance < 0)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public static float AngleDir(Vector2 controllerPos, Vector2 bolaPos)
    {
        return -controllerPos.x * bolaPos.y + controllerPos.y * bolaPos.x;
    }
}
