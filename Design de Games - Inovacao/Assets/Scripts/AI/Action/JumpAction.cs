using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gerador/AI/Action/Jump")]
public class JumpAction : Actions
{
    public override void Act(StateController controller)
    {
        Jump(controller);
    }

    private void Jump(StateController controller)
    {
		Vector3 controllerPos = controller.transform.position;
		Vector3 coletaPos = controller.wayPointList[(int)controller.botScore.Value].transform.position;

		if (controller.canJump == true)
        {
            controller.rb.AddForce(new Vector2(0, controller.botStats.jumpForce * (coletaPos.y - controllerPos.y) * 0.4f), ForceMode2D.Impulse);
        }
    }

}
