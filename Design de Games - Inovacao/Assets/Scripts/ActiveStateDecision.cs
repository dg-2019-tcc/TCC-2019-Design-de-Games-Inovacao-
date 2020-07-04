using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "ScriptableObjects/Decisions/ActiveState")]
public class ActiveStateDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool ex = controller.target.gameObject.activeSelf;
        return ex;
    }
}
