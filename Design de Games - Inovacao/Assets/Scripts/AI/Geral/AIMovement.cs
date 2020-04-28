using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : RaycastController
{

	float maxClimbAngle = 80;
	float maxDescendAngle = 80;
	private StateController controller;
	//public TriggerCollisionInfo collisions;
	public CollisionInfo collisions;

	public GameObject ai;

    public float turnCooldown;


	public bool isBallGame;
	public BoxCollider2D boxCollider;

	public float hitLenght = 5f;

	//Vector2 botInput;


	public bool isColteta;

	// Start is called before the first frame update
	public override void Start()
	{
		base.Start();
		controller = GetComponent<StateController>();
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
		}
		if (moveAmount.y != 0)
		{
			VerticalCollisions(ref moveAmount);
		}

		transform.Translate(moveAmount);
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
		float directionY = Mathf.Sign(moveAmount.y);
		float rayLenght = Mathf.Abs(moveAmount.y) + skinWidth;

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
					

					if (directionY == 1 || hit.distance == 0)
					{
						continue;
					}
					if (collisions.fallingPlatform)
					{
						continue;
					}

					if (moveAmount.y < -0.5)
					{
						collisions.fallingPlatform = true;
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
