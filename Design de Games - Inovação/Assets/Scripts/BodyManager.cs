﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : MonoBehaviour
{
    public RuntimeAnimatorController[] anim;

    

    public void Start()
    {

        int index = PlayerPrefs.GetInt("bodyIndex");

        this.GetComponent<Animator>().runtimeAnimatorController = anim[index];

    }
}
