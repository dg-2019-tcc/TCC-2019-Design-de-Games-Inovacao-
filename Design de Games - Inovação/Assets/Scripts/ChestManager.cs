using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public RuntimeAnimatorController[] anim;


    public void Start()
    {

        int index = PlayerPrefs.GetInt("chestIndex");

        this.GetComponent<Animator>().runtimeAnimatorController = anim[index];

    }
}
