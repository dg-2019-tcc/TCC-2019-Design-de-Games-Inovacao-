using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gerador/AI/Action/Futebol")]
public class FutebolAction : Actions
{
    public override void Act(StateController controller)
    {
        Futebol(controller);
    }

    private void Futebol(StateController controller)
    {


        Vector2 controllerPos = controller.transform.position;
        Vector2 coletaPos = controller.wayPointList[0].transform.position;



        float distance = AngleDir(controllerPos, coletaPos);
        Debug.Log(distance);

        if(distance > 0)
        {
            //controller.target = controller.wayPointList[0].transform;
            controller.rb.velocity = new Vector2(-controller.botStats.moveSpeed, 0);
        }

        else if(distance < 0)
        {
            //controller.target = controller.wayPointList[1].transform;
            controller.rb.velocity = new Vector2(controller.botStats.moveSpeed, 0);
        }

        /*float step = controller.botStats.moveSpeed * Time.deltaTime;

        controller.transform.position = Vector3.MoveTowards(controller.transform.position, controller.target.position, step);*/
    }

    public static float AngleDir(Vector2 controllerPos, Vector2 coletaPos)
    {
        return -controllerPos.x * coletaPos.y + controllerPos.y * coletaPos.x;
    }
}
