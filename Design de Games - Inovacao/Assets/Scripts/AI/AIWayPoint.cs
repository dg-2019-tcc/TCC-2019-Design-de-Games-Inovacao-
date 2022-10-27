using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWayPoint : MonoBehaviour
{
    public StateController aiController;
    public int wayPointIndex;
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("AI"))
        {
            aiController = col.GetComponent<StateController>();
            aiController.nextWayPoint = wayPointIndex;
            Destroy(gameObject);


        }
    }


}
    