using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gerador/AI/Decision/Obstacle")]
public class ObstacleDecision : Decision
{
	public override bool Decide(StateController controller)
	{
		bool targetNotVisible = Look(controller);
		return targetNotVisible;
	}

	private bool Look(StateController controller)
	{
		Vector3 controllerPos = controller.transform.position;
		Vector3 coletaPos = controller.wayPointList[controller.nextWayPoint].transform.position;

		RaycastHit hit;
		if (!Physics.Raycast(controllerPos, controller.transform.TransformDirection(Vector3.forward), out hit, 3))
		{
			Debug.DrawRay(controller.transform.position, controller.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
			//controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
			return false;
		}
		else if (controller.rb.velocity.magnitude != 0)
		{
			Debug.DrawRay(controller.transform.position, controller.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
			return false;
		}
		else
		{
			Debug.DrawRay(controllerPos, controller.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
			return true;
		}
	}
}
