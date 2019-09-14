using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : MonoBehaviour
{
    public RuntimeAnimatorController[] anim;

    public CustomManager index;
    

    public void ChangeAnimatorController()
    {

        this.GetComponent<Animator>().runtimeAnimatorController = anim[index.bodyIndex];

    }
}
