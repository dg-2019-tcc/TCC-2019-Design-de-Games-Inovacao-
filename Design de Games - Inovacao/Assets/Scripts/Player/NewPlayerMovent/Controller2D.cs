﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

public class Controller2D : RaycastController
{
    float maxClimbAngle = 80;
    float maxDescendAngle = 80;

    public CollisionInfo collisions;

    InputController inputController;
    DogController dogController;
    Vector2 playerInput;

	private float joyGambiarra;

    #region Unity Function

    public override void Start()
    {
        base.Start();
        inputController = GetComponent<InputController>();
        dogController = GetComponent<DogController>();
    }

    private void LateUpdate()
    {
        joyGambiarra = playerInput.y;
    }

    #endregion

    #region Public Functions

    public void Move(Vector2 moveAmount)
    {
        UpdateRaycastOrigins();
        collisions.Reset();
        collisions.velocityOld = moveAmount;

        //playerInput = input;
        playerInput = inputController.joyInput;

        if (moveAmount.y < 0)
        {
            DescendSlope(ref moveAmount);
        }
        if (moveAmount.x != 0)
        {
            HorizontalCollisions(ref moveAmount);
        }
        if (moveAmount.y != 0)
        {
            VerticalCollisions(ref moveAmount);
        }

        transform.Translate(moveAmount);
    }

    #endregion

    #region Private Functions

    void HorizontalCollisions(ref Vector2 moveAmount)
    {
        float directionX = Mathf.Sign(moveAmount.x);
        float rayLenght = Mathf.Abs(moveAmount.x) + skinWidth;


        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLenght, collisionMask);

            //Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

            if (hit)
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

                if (hit.collider.CompareTag("Barreira"))
                {
                    collisions.bateuObs = true;
                }

                if (hit.collider.CompareTag("Through"))
                {

                    if (dogController.state == DogController.State.Pipa)
                    {
                        continue;
                    }
                    if (collisions.isFalling)
                    {
                        continue;
                    }
                    if (directionX == 1 && Mathf.Abs(playerInput.x) != 0)
                    {
                        continue;
                    }

                    if (hit.distance == 0)
                    {
                        continue;
                    }
                    //if (directionX == 1 && /*hit.distance == 0 && */Mathf.Abs(playerInput.x) != 0 && !collisions.descendingSlope && !collisions.climbingSlope && moveAmount.y !=0 )
                    //{
                    //    continue;
                    //}
                    if (collisions.fallingPlatform && collisions.climbingSlope == false || collisions.isFalling)
                    {
                        continue;
                    }

                    if (playerInput.y < joyGambiarra)
                    {
                        if (playerInput.y < -0.9)
                        {
                            collisions.isFalling = true;
                            collisions.fallingPlatform = true;
                            Invoke("ResetFallingPlatform", 0.1f);
                            continue;
                        }
                    }
                }


                if (i == 0 && slopeAngle <= maxClimbAngle)
                {
                    if (collisions.descendingSlope)
                    {
                        collisions.descendingSlope = false;
                        moveAmount = collisions.velocityOld;
                    }

                    float distanceToSlopeStart = 0;
                    if (slopeAngle != collisions.slopeAngleOld)
                    {
                        distanceToSlopeStart = hit.distance - skinWidth;
                        moveAmount.x -= distanceToSlopeStart * directionX;
                    }
                    ClimbSlope(ref moveAmount, slopeAngle);
                    moveAmount.x += distanceToSlopeStart * directionX;
                }

                if (!collisions.climbingSlope || slopeAngle > maxClimbAngle)
                {

                    moveAmount.x = (hit.distance - skinWidth) * directionX;
                    rayLenght = hit.distance;

                    if (collisions.climbingSlope)
                    {
                        moveAmount.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(moveAmount.x);
                    }

                    collisions.left = directionX == -1;
                    collisions.right = directionX == 1;
                }
            }
        }
    }

    void VerticalCollisions(ref Vector2 moveAmount)
    {
        float directionY = Mathf.Sign(moveAmount.y);
        float rayLenght = Mathf.Abs(moveAmount.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + moveAmount.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLenght, collisionMask);

            //Debug.DrawRay(rayOrigin , Vector2.up * directionY, Color.red);

            if (hit)
            {
                if (hit.collider.CompareTag("Destroy"))
                {
                    hit.collider.gameObject.SendMessage("ToAqui");
                }

                if (hit.collider.CompareTag("Through"))
                {
                    if (dogController.state == DogController.State.Pipa)
                    {
                        collisions.isFalling = true;
                        continue;
                    }

                    if (directionY == 1 || hit.distance == 0 || collisions.isFalling)
                    {
                        collisions.isFalling = true;
                        continue;
                    }
                    if (collisions.fallingPlatform)
                    {
                        collisions.isFalling = true;
                        continue;
                    }

                    if (playerInput.y < joyGambiarra)
                    {
                        if (playerInput.y < -0.9)
                        {
                            collisions.isFalling = true;
                            collisions.fallingPlatform = true;
                            Invoke("ResetFallingPlatform", 0.1f);
                            continue;
                        }

                    }

                }

                moveAmount.y = (hit.distance - skinWidth) * directionY;
                rayLenght = hit.distance;

                if (collisions.climbingSlope)
                {
                    moveAmount.x = moveAmount.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(moveAmount.x);
                }

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;

            }

        }
        //joyGambiarra = playerInput.y;

        if (collisions.climbingSlope)
        {
            float directionX = Mathf.Sign(moveAmount.x);
            rayLenght = Mathf.Abs(moveAmount.x) + skinWidth;
            Vector2 rayOrigin = ((directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight) + Vector2.up * moveAmount.y;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLenght, collisionMask);

            if (hit)
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if (slopeAngle != collisions.slopeAngle)
                {
                    moveAmount.x = (hit.distance - skinWidth) * directionX;
                    collisions.slopeAngle = slopeAngle;
                }
            }
        }


    }

    void ClimbSlope(ref Vector2 moveAmount, float slopeAngle)
    {
        float moveDistance = Mathf.Abs(moveAmount.x);
        float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
        collisions.climbingSlope = true;
        if (moveAmount.y <= climbVelocityY)
        {
            moveAmount.y = climbVelocityY;
            moveAmount.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(moveAmount.x);

            collisions.below = true;
            collisions.climbingSlope = true;
            collisions.slopeAngle = slopeAngle;
        }
    }

    void DescendSlope(ref Vector2 moveAmount)
    {
        float directionX = Mathf.Sign(moveAmount.x);
        Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);

        if (hit)
        {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeAngle != 0 && slopeAngle <= maxDescendAngle)
            {
                if (Mathf.Sign(hit.normal.x) == directionX)
                {
                    if (hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(moveAmount.x))
                    {
                        float moveDistance = Mathf.Abs(moveAmount.x);
                        float descendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

                        moveAmount.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(moveAmount.x);
                        moveAmount.y -= descendVelocityY;

                        collisions.slopeAngle = slopeAngle;
                        collisions.descendingSlope = true;
                        collisions.below = true;
                    }
                }
            }
        }
    }

    void ResetFallingPlatform()
    {
        collisions.fallingPlatform = false;
    }

    #endregion

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;

        public bool climbingSlope;
        public bool descendingSlope;

        public Vector2 velocityOld;

        public float slopeAngle, slopeAngleOld;

        public bool fallingPlatform;

        public bool isDoor;
        public bool isFalling;

        public bool bateuObs;

        public void Reset()
        {
            above = below = false;
            left = right = false;
            climbingSlope = false;
            descendingSlope = false;
            isDoor = isFalling = false;
            bateuObs = false;

            slopeAngleOld = slopeAngle;
            slopeAngle = 0;
        }
    }

}
