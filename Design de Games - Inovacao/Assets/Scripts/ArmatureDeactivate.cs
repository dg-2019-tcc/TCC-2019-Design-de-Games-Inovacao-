using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class ArmatureDeactivate : MonoBehaviour
{
    public UnityArmatureComponent unityArmature;
    public Armature armature = null;
    public UnityDragonBonesData data;

    /*private void Start()
    {
        armature = unityArmature.armature;
        unityArmature.armature.clock.Remove(unityArmature.armature);
    }*/

    private void OnEnable()
    {
        unityArmature.armature.AdvanceTime(0);
            Debug.Log("Armature Activate" + gameObject);
    }

    /*private void OnDisable()
    {
        //data = unityArmature.unityData;
        if (armature.clock != null)
        {
            unityArmature.armature.clock.Remove(unityArmature.armature);
        }
        Debug.Log("Armature Deactivate" + gameObject);
    }*/

}
