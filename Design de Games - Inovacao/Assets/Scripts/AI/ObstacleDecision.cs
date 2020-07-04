using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AI/Decision/Obstacle")]
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

		RaycastHit2D hit = Physics2D.Raycast(controllerPos, controller.transform.right, 3, LayerMask.GetMask("Obstacle"));

		Debug.DrawRay(controllerPos, controller.transform.right.normalized * 3, Color.yellow);

		if (hit
			&& hit.collider.CompareTag("Obstacle"))
		{
			
			//controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
			return true;
		}
		else
		{
			
			return false;
		}
	}
}
