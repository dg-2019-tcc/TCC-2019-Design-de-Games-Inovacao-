using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : RaycastController
{
    public float speed = 3f;
    public float jumpVelocity = 1f;
    public bool isJumping;

    Vector3 velocity;

    float maxClimbAngle = 80;
	float maxDescendAngle = 80;
	private StateController controller;
	//public TriggerCollisionInfo collisions;
	public CollisionInfo collisions;
    public AITriggerController triggerController;

	public GameObject ai;

    public float turnCooldown;


	public bool isBallGame;
	public BoxCollider2D boxCollider;

	public float hitLenght = 5f;

    private ColetavelGerador coletavelGerador;
    private Vector2 coletavelPos;

	//Vector2 botInput;
    public PlatformEffector2D[] effectors;

    public bool isColteta;
    float newVel = 0.01f;
    private bool found;
    public float maxSpeed = 1f;
   
    Transform target;

    // Start is called before the first frame update
    public override void Start()
	{
		base.Start();
		controller = GetComponent<StateController>();
        target = controller.wayPointList[0];

        effectors = FindObjectsOfType<PlatformEffector2D>();

        /*if (isColteta)
        {
            coletavelGerador = FindObjectOfType<ColetavelGerador>();
        }*/
	}

    public void Update()
    {
        if (isColteta)
        {
            Vector2 vel = controller.rb.velocity;

            if (vel.magnitude > maxSpeed)
            {
                controller.rb.velocity = vel.normalized * maxSpeed;
            }

            if (triggerController.triggerCollision.isRight || triggerController.triggerCollision.isLeft || triggerController.triggerCollision.isUp || triggerController.triggerCollision.isDown)
            {
                found = true;
            }

            else
            {
                found = false;
            }

            if (collisions.right == true)
            {
                Debug.Log("Right");
                newVel = -0.01f;
            }
            else if (collisions.left == true)
            {
                Debug.Log("Left");
                newVel = 0.01f;
            }
            //velocity.y += gravity * Time.deltaTime;
            velocity.x += speed;
            triggerController.RayTriggerDirection();
            Move(controller.rb.velocity * Time.deltaTime);
            //coletavelPos = coletavelGerador.coletavelCerto.transform.position;

            //Vector2 aiPos = transform.position;
            if (collisions.below == true)
            {
                isJumping = false;
            }

            if (triggerController.triggerCollision.isRight)
            {
                GoRight();
                Move(controller.rb.velocity * Time.deltaTime);
                //GoHorizontal(1);
            }

            else if (triggerController.triggerCollision.isLeft)
            {
                GoLeft();
                Move(controller.rb.velocity * Time.deltaTime);
                //GoHorizontal(-1);
            }

            else if (triggerController.triggerCollision.isUp)
            {
                for(int i =0; i< effectors.Length; i++)
                {
                    effectors[i].rotationalOffset = 0f;
                }
                GoUp();
            }

            else if (triggerController.triggerCollision.isDown)
            {

                for (int i = 0; i < effectors.Length; i++)
                {
                    effectors[i].rotationalOffset = 180f;
                }
                GoDown();
            }


            else if (found == false)
            {
                if (transform.position.x - target.transform.position.x > 1)
                {
                    GoLeft();
                }

                else if (transform.position.x - target.transform.position.x < -1)
                {
                    GoRight();
                }

                else
                {
                    if (target == controller.wayPointList[0])
                    {
                        target = controller.wayPointList[1];
                    }

                    else
                    {
                        target = controller.wayPointList[0];
                    }
                }



            }
        }
  
    }

    public void GoRight()
    {
        controller.rb.velocity = new Vector2(velocity.x, 0);
    }

    public void GoLeft()
    {
        controller.rb.velocity = new Vector2(-velocity.x, 0);
    }


    public void GoUp()
    {
        isJumping = true;
        controller.rb.AddForce(new Vector2(0f, 50f), ForceMode2D.Impulse);
        Move(controller.rb.velocity * Time.deltaTime);
    }

    public void GoDown()
    {
        controller.rb.AddForce(new Vector2(0f, -10f), ForceMode2D.Impulse);
        Move(velocity * Time.deltaTime);
    }


    public void Move(Vector2 moveAmount)
	{
		UpdateRaycastOrigins();
		collisions.Reset();
		collisions.velocityOld = moveAmount;

		//botInput = input;

		if (moveAmount.y < 0)
		{
			DescendSlope(ref moveAmount);
		}
		if (moveAmount.x != 0)
		{
			HorizontalCollisions(ref moveAmount);
            Vector2 all = new Vector2(0, -1);
            VerticalCollisions(ref all);
        }
		if (controller.rb.velocity.y <= 0)
		{
            Vector2 all = new Vector2(0,-1);
			VerticalCollisions(ref moveAmount);
		}

		//transform.Translate(moveAmount);
	}

	void HorizontalCollisions(ref Vector2 moveAmount)
	{
		float directionX = Mathf.Sign(moveAmount.x);
		float rayLenght = Mathf.Abs(moveAmount.x) + skinWidth;


		for (int i = 0; i < horizontalRayCount; i++)
		{
			Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLenght, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

			if (hit)
			{
				float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

				if (hit.collider.tag == "Through")
				{

					
					if (directionX == 1 || hit.distance == 0)
					{
						continue;
					}
					if (collisions.fallingPlatform)
					{
						continue;
					}

					if (moveAmount.x < -0.5 || moveAmount.x > 0.5 && collisions.climbingSlope == false && collisions.descendingSlope == false)
					{
						collisions.fallingPlatform = true;
						Invoke("ResetFallingPlatform", 0.1f);
						continue;
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
        float directionY;

        if (moveAmount.y >= 0)
        {
            directionY = 1f;
        }
        else
        {
            directionY = -1f;
        }
		
		float rayLenght = Mathf.Abs(directionY) + skinWidth;

		for (int i = 0; i < verticalRayCount; i++)
		{
			Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i + moveAmount.x);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLenght, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

			if (hit)
			{

				if (hit.collider.tag == "Through")
				{
                    if (triggerController.triggerCollision.isUp)
                    {
                        continue;
                    }

					/*if (directionY > 0  || hit.distance == 0)
					{
						continue;
					}*/
					/*if (collisions.fallingPlatform)
					{
						continue;
					}

					/*if (moveAmount.y < -0.5)
					{
						collisions.fallingPlatform = true;
						Invoke("ResetFallingPlatform", 0.1f);
						continue;
					}*/

                    if (triggerController.triggerCollision.isDown)
                    {

                        collisions.fallingPlatform = true;
                        collisions.below = false;
                        Invoke("ResetFallingPlatform", 0.1f);
                        continue;
                    }
				}

				moveAmount.y = (hit.distance - skinWidth) * directionY;
				rayLenght = hit.distance;

				if (collisions.climbingSlope)
				{
					moveAmount.x = moveAmount.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(moveAmount.x);
				}

				collisions.below = directionY == -1;
                collisions.below = true;
				collisions.above = directionY == 1;

			}

		}

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

		public void Reset()
		{
			above = below = false;
			left = right = false;
			climbingSlope = false;
			descendingSlope = false;
			isDoor = false;

			slopeAngleOld = slopeAngle;
			slopeAngle = 0;
		}
	}


}
