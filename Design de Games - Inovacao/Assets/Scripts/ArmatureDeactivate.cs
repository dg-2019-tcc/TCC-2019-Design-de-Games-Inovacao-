using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class ArmatureDeactivate : MonoBehaviour
{
    public UnityArmatureComponent unityArmature;
    public Armature armature = null;
    public UnityDragonBonesData data;

    private void Start()
    {
        armature = unityArmature.armature;
        unityArmature.armature.clock.Remove(unityArmature.armature);
    }

    private void OnEnable()
    {
        if(armature.clock == null)
        {
            DragonBones.UnityFactory.factory.clock.Add(armature);
        }
    }

    private void OnDisable()
    {
        if (armature.clock != null)
        {
            unityArmature.armature.clock.Remove(unityArmature.armature);
        }
    }

}
