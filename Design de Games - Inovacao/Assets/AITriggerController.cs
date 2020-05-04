using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITriggerController : RaycastController
{
    public TriggerCollisionInfo triggerCollision;


    public void RayTriggerDirection()
    {
        UpdateRaycastOrigins();
        triggerCollision.Reset();

        RightCollisions();
        LeftCollisions();
        UpCollisions();
        DownCollisions();
    }

    void RightCollisions()
    {
        float directionX = 1;
        float rayLenght;

        rayLenght = 20f + skinWidth;
        

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLenght, collisionMask);

            //Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

            if (hit)
            {
                if (hit.collider.tag == "Coletavel")
                {
                    triggerCollision.isRight = true;
                }
            }
        }
    }

    void LeftCollisions()
    {
        float directionX = -1;
        float rayLenght;

        rayLenght = 20f + skinWidth;
        

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLenght, collisionMask);

            //Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

            if (hit)
            {
                if (hit.collider.tag == "Coletavel")
                {
                    triggerCollision.isLeft = true;
                }
            }
        }
    }

    void UpCollisions()
    {
        float directionY = 1;
        float rayLenght = 20f + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLenght, collisionMask);

            //Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

            if (hit)
            {
                if (hit.collider.tag == "Coletavel")
                {
                    triggerCollision.isUp = true;
                }
            }
        }
    }

    void DownCollisions()
    {
        float directionY = -1;
        float rayLenght = 20f + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLenght, collisionMask);

            //Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

            if (hit)
            {
                if (hit.collider.tag == "Coletavel")
                {
                    triggerCollision.isDown = true;
                }
            }
        }
    }

    public struct TriggerCollisionInfo
    {
        public bool isLeft, isRight;
        public bool isUp, isDown;

        public void Reset()
        {
            isLeft = isRight = false;
            isUp = isDown = false;
        }
    }
}
