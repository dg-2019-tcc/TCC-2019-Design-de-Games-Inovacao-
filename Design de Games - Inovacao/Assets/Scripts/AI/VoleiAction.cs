using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gerador/AI/Action/Volei")]
public class VoleiAction : Actions
{
    public override void Act(StateController controller)
    {
        Volei(controller);
    }

    private void Volei(StateController controller)
    {
        Vector3 controllerPos = controller.transform.position;
        Vector3 coletaPos = controller.wayPointList[0].transform.position;

        float distance =Mathf.Abs(coletaPos.x - controllerPos.x);
        Debug.Log(distance);


        if (distance <= 3)
        {
            controller.target = controller.wayPointList[0].transform;
        }

        else
        {
            controller.target = controller.wayPointList[1].transform;
        }


        float step = controller.enemyStats.moveSpeed * Time.deltaTime;

        controller.transform.position = Vector3.MoveTowards(controller.transform.position, controller.target.position, step);

    }
}
