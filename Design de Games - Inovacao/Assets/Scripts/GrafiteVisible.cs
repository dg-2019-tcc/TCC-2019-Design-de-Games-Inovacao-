using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;

public class GrafiteVisible : MonoBehaviour
{
    public MeshRenderer mesh;
    public Skeleton skeleton;
    public bool isVisible = false;


    /*private void OnBecameVisible()
    {
        isVisible = true;
        Debug.Log("Visible");
        mesh.enabled = true;
        //skeleton.
    }

    private void OnBecameInvisible()
    {
        Debug.Log("Invisible");
        mesh.enabled = false;
    }*/
}
