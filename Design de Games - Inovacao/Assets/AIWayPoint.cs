using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWayPoint : MonoBehaviour
{
    public StateController aiController;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("AI"))
        {
            Debug.Log("Passou");
            aiController = col.GetComponent<StateController>();
            aiController.nextWayPoint++;
            Destroy(gameObject);

        }
    }


}
