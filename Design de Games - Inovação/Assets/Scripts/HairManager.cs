using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairManager : MonoBehaviour
{
    public RuntimeAnimatorController[] anim;



    public void Start()
    {

        int index = PlayerPrefs.GetInt("hairIndex");

        this.GetComponent<Animator>().runtimeAnimatorController = anim[index];

    }
}
