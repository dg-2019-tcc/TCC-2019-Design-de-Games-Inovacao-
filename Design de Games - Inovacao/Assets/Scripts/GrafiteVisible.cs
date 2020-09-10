using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrafiteVisible : MonoBehaviour
{
    public GameObject obj;
    private void OnBecameVisible()
    {
        obj.SetActive(true);
    }

    private void OnBecameInvisible()
    {
        obj.SetActive(false);
    }
}
