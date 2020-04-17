using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gerador/AI/Decision/Stop")]
public class ColetaStopDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool canStop = Stop(controller);
        return canStop;
    }

    private bool Stop(StateController controller)
    {
        Vector2 aiPos = controller.transform.position;
        Vector2 coletavelPos = controller.wayPointList[controller.nextWayPoint].transform.position;

        if(aiPos.x - coletavelPos.x <=0.5f && aiPos.x - coletavelPos.x > -0.5f)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
    }
