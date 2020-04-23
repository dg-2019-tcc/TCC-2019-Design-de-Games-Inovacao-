using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollisionsController : RaycastController
{

    public TriggerCollisionInfo collisions;

    private PortaManager porta;

    public override void Start()
    {
        base.Start();
    }


    public void MoveDirection(Vector2 dir)
    {
        UpdateRaycastOrigins();
        collisions.Reset();

        if (dir.x > 0)
        {
            RightCollisions();
        }
        else if (dir.x < 0)
        {
            LeftCollisions();
        }
        else if (dir.y > 0)
        {
            UpCollisions();
        }
        else
        {
            DownCollisions();
        }
    }

    void RightCollisions()
    {
        float directionX = 1;
        float rayLenght = 0.1f + skinWidth;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLenght, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

            if (hit)
            {
                if(hit.collider.tag == "Porta")
                {
                    Debug.Log("Right");
                    porta = hit.collider.GetComponent<PortaManager>();
                    porta.OpenDoor();
                    return;
                }
            }
        }
    }

    void LeftCollisions()
    {
        float directionX = -1;
        float rayLenght = 0.1f + skinWidth;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLenght, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

            if (hit)
            {
                if (hit.collider.tag == "Porta")
                {
                    Debug.Log("left");
                    hit.collider.GetComponent<PortaManager>().OpenDoor();
                    continue;
                }
            }
        }
    }

    void UpCollisions()
    {
        float directionY = 1;
        float rayLenght = 0.1f + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLenght, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

            if (hit)
            {
                if (hit.collider.tag == "Porta")
                {
                    Debug.Log("up");
                    hit.collider.GetComponent<PortaManager>().OpenDoor();
                    continue;
                }
            }
        }
    }

    void DownCollisions()
    {
        float directionY = -1;
        float rayLenght = 0.1f + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLenght, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

            if (hit)
            {
                if (hit.collider.tag == "Porta")
                {
                    Debug.Log("down");
                    hit.collider.GetComponent<PortaManager>().OpenDoor();
                    continue;
                }
            }
        }
    }

    public struct TriggerCollisionInfo
    {
        public bool isDoor;

        public void Reset()
        {
            isDoor = false;
        }
    }
}
