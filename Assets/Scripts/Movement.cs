using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : RaycastController
{
    private Animator animator;

    [SerializeField]
    float vel;
    [SerializeField]
    [Tooltip("This value should be a positive integer")]
    float mouseOffsetZ = 20;
    
    private Vector3 mousePosition;
    private Vector3 trueMousePosition;
    private bool clickedOnce = false;

    public CollisionInfo collisions;

    // Start is called before the first frame update
    void Start()
    {
        trueMousePosition = player.position;
        animator = player.GetComponent<Animator>();
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, Mathf.Infinity);
            trueMousePosition = hit.point;

            mousePosition = new Vector3(hit.point.x, hit.point.y, player.position.z);
            clickedOnce = true;
        }

        Vector3 newPos = new Vector3(player.position.x, player.position.y, trueMousePosition.z - mouseOffsetZ);

        if (Input.GetMouseButtonUp(0))
        {
            clickedOnce = false;
        }

        Vector2 mousePos2D = new Vector2(trueMousePosition.x, trueMousePosition.y);

        MovePlayer();
        MoveSprite(); // Possibly make position private
    }

    private void MovePlayer()
    {
        UpdateRaycastOrigins();
        collisions.Reset();
        
        Vector3 movePoint = mousePosition;

        if (movePoint.x != player.position.x) {
            HorizontalCollisions(ref movePoint);
        }

        if (movePoint.y != player.position.y) {
            VerticalCollisions(ref movePoint);
        }

        // XY velocity
        // Almost the same as player.Translate(velocity);
        player.position = Vector3.MoveTowards(player.position, movePoint, vel);
        mousePosition = movePoint;

        // Z velocity
        movePoint = new Vector3(player.position.x, player.position.y, trueMousePosition.z - mouseOffsetZ);
        player.position = Vector3.MoveTowards(player.position, movePoint, vel);
    }

    public void HorizontalCollisions(ref Vector3 movePoint) 
    {
        float distance = mousePosition.x - player.position.x;
        float directionX = Mathf.Sign(distance);
		float rayLength = Mathf.Abs(distance) + skinWidth;

		for (int i = 0; i < horizontalRayCount; i ++) {
			Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft :
            raycastOrigins.bottomRight;
			
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX,
            rayLength, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

			if (hit) {
                Debug.Log("Horizontal hit");
                rayLength = hit.distance;

                collisions.left = (directionX == -1);
                collisions.right = (directionX == 1);

                Bounds bounds = playerCollider.bounds;
                float hitPoint = hit.point.x - (skinWidth + bounds.extents.x) * directionX;

                if (directionX == 1)
                {
                    if (hitPoint < movePoint.x)
                    {
                        movePoint.x =  hitPoint;
                    }
                }
                else if (directionX == -1)
                {
                    if (hitPoint > movePoint.x)
                    {
                        movePoint.x = hitPoint;
                    }
                }
            }
		}
	}

    public void VerticalCollisions(ref Vector3 movePoint)
    {
        float distanceX = mousePosition.x - player.position.x;
        float directionX = Mathf.Sign(distanceX);
        float distanceY = mousePosition.y - player.position.y;
		float directionY = Mathf.Sign(distanceY);
		float rayLength = Mathf.Abs(distanceY) + skinWidth;

		for (int i = 0; i < verticalRayCount; i ++) {
			Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : 
                raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY,
                rayLength, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.white);

			if (hit) {
                Debug.Log("Vertical hit");
                rayLength = hit.distance;

                collisions.below = (directionY == -1);
                collisions.above = (directionY == 1);

                Bounds bounds = playerCollider.bounds;
                float hitPoint = hit.point.y - (skinWidth + bounds.extents.y) * directionY;

                if (directionY == 1)
                {
                    if (hitPoint < movePoint.y)
                    {
                        movePoint.y =  hitPoint;
                    }
                }
                else if (directionY == -1)
                {
                    if (hitPoint > movePoint.y)
                    {
                        movePoint.y =  hitPoint;
                    }
                }
            }
		}
	}

    private void MoveSprite(){
        if(player.position != mousePosition)
        {
            animator.SetBool("move", true);
        }
        else
        {
            animator.SetBool("move", false);
        }
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;

        public void Reset() {
            above = below = false;
            left = right = false;
        }
    }
}
