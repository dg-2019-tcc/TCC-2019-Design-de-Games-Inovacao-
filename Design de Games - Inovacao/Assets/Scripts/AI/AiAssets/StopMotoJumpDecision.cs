using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMotoJumpDecision : Decision
{
	public override bool Decide(StateController controller)
	{
		bool targetDown = StopJump(controller);
		return targetDown;
	}

	private bool StopJump(StateController controller)
	{
		Vector3 controllerPos = controller.transform.position;
		Vector3 coletaPos = controller.wayPointList[controller.nextWayPoint].transform.position;

		RaycastHit2D hit = Physics2D.Raycast(controllerPos, -controller.transform.up, 3, LayerMask.GetMask("Default"));

		Debug.DrawRay(controllerPos, -controller.transform.up.normalized * 3, Color.yellow);

		if (hit)// && hit.collider.CompareTag("Plataforma"))
		{
			return true;
		}

		else
		{
			return false;
		}
	}
}
