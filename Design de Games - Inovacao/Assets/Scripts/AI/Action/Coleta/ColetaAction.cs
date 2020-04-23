using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gerador/AI/Action/Coleta")]
public class ColetaAction : Actions
{
    public override void Act(StateController controller)
    {
        Coleta(controller);
    }

    private void Coleta(StateController controller)
    {
		controller.target = controller.wayPointList[(int)controller.botScore.Value].transform;

		float step = controller.botStats.moveSpeed  * Time.deltaTime;

		if (controller.transform.position.x - controller.target.position.x > 0)                         //moveesquerda
		{
			controller.rb.velocity = new Vector2(-controller.botStats.moveSpeed, controller.rb.velocity.y);
		}
		else
		{
			controller.rb.velocity = new Vector2(controller.botStats.moveSpeed, controller.rb.velocity.y);
		}
		
    }
}

