using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Gerador/AI/Decision/Look")]
public class LookDecision : Decision
{
   public override bool Decide(StateController controller)
    {
        bool targetNotVisible = Look(controller);
        return targetNotVisible;
    }

    private bool Look(StateController controller)
    {
        Vector3 controllerPos = controller.transform.position;
        Vector3 coletaPos = controller.wayPointList[(int)controller.botScore.Value].transform.position;


		if (coletaPos.y - controllerPos.y >= 0.2f)
		{
			return true;
		}
		else if (coletaPos == null)
		{
			return true;
		}

		else
		{
			return false;
		}
    }
}
