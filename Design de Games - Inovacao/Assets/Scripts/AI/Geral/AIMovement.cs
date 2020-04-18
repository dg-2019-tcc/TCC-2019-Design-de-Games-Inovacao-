using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    private StateController controller;

    public GameObject ai;

    public float turnCooldown;

    public bool isColteta;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<StateController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!isColteta)
        {
            if (controller.rb.velocity.x < 0f)
            {
                Debug.Log("Esquerda");
                Quaternion direction = Quaternion.Euler(0, 180, 0);
                ai.transform.rotation = direction;
            }

            else if (controller.rb.velocity.x > 0f)
            {
                Quaternion direction = Quaternion.Euler(0, 0, 0);
                ai.transform.rotation = direction;
            }

            else
            {
                Quaternion direction = Quaternion.Euler(0, 180, 0);
                ai.transform.rotation = direction;
            }
        }

        else
        {
            float aiPosX = Mathf.Abs(controller.transform.position.x);
            float bolaPosX = Mathf.Abs(controller.wayPointList[controller.nextWayPoint].transform.position.x);

            float distance = aiPosX - bolaPosX;

            if(distance < 0)
            {
                Quaternion direction = Quaternion.Euler(0, 180, 0);
                ai.transform.rotation = direction;
            }

            else
            {
                Quaternion direction = Quaternion.Euler(0, 0, 0);
                ai.transform.rotation = direction;

            }
        }
    }


}
