﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfMovement : MonoBehaviour
{
    public GameObject player;
    public float targetDistance;
    public float allowedDistance = 5;
    public GameObject wolf;
    public float followSpeed;
    public RaycastHit shot;
    Animator wolfAnim;
	


    private Vector3 oldPosition;


	
    void Start()
    {
        wolfAnim = gameObject.transform.GetChild(0).GetComponent<Animator>();
	}


    // Update is called once per frame
    void Update()
    {
        // transform.position = Vector2.MoveTowards(transform.position, player.transform.position, followSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, player.transform.position) > allowedDistance)
        {
            followSpeed = 0.1f;
            wolfAnim.SetBool("isWalking", true);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, followSpeed);

            



            if (transform.position.x > player.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);

            }
            else if (transform.position.x < player.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }


        }


        else
        {

            followSpeed = 0;

            wolfAnim.SetBool("isWalking", false);

        }



        
    }

        
}
