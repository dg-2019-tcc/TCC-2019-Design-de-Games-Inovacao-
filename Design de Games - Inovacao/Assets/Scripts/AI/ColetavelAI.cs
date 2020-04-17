﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColetavelAI : MonoBehaviour
{
    private StateController aiController;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("AI"))
        {
            aiController = GetComponent<StateController>();
            Debug.Log(aiController.nextWayPoint);
            aiController.nextWayPoint++;
            Debug.Log(aiController.nextWayPoint);

        }
    }
}
