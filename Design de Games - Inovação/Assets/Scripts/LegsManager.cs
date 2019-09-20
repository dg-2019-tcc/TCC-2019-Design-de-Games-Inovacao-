using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsManager : MonoBehaviour
{
    public RuntimeAnimatorController[] anim;


    public void Start()
    {

        int index = PlayerPrefs.GetInt("legsIndex");

        this.GetComponent<Animator>().runtimeAnimatorController = anim[index];

    }
}
