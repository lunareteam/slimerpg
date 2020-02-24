using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : RaycastController
{
    [SerializeField] float vel;
    [SerializeField] Vector3 playerVelocity;
    private Vector3 mousePosition;
    private Vector3 trueMousePosition;
    private bool clickedOnce = false;

    public CollisionInfo collisions;

    // Start is called before the first frame update
    void Start()
    {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log(ray.ToString());
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, Mathf.Infinity);
            Debug.Log(hit.collider);
            trueMousePosition = hit.point;

            mousePosition = new Vector3(hit.point.x, hit.point.y, player.position.z);
            Debug.Log(mousePosition);
            clickedOnce = true;
        }

        if (Vector3.Distance(mousePosition, player.position) < vel)
        {
            clickedOnce = false;
        }

        Vector2 mousePos2D = new Vector2(trueMousePosition.x, trueMousePosition.y);

        MovePlayer(mousePos2D);
        MoveSprite(); // Possibly make position private
    }

    private void MovePlayer(Vector2 mousePos)
    {
        UpdateRaycastOrigins();
        collisions.Reset();
        
        // Calculate angle
        Vector2 newCoordSysPos = new Vector2(mousePos.x - player.transform.position.x,
        mousePos.y - player.transform.position.y);
        float angle = Mathf.Deg2Rad * Vector2.Angle(Vector2.right, newCoordSysPos);
        
        // Full 360 degrees
        if ((mousePos.y < player.position.y) || (mousePos.y == player.position.y &&
        mousePos.x < player.position.x))
        {
            angle *= -1;
        }

        float velX = clickedOnce ? Mathf.Cos(angle) * vel : 0f;
        float velY = clickedOnce ? Mathf.Sin(angle) * vel : 0f;
        Vector3 velocity = new Vector3(velX, velY, 0f);
        playerVelocity = velocity;

        // Visual debug
        Vector2 rayOrigin = raycastOrigins.bottomLeft;
        Debug.DrawRay(rayOrigin, Vector2.right * velX * 10f, Color.blue);
        Debug.DrawRay(rayOrigin, Vector2.up * velY * 10f, Color.red);
        playerVelocity = velocity;
        
        collisions.velocityOld = velocity;

        if (velocity.x != 0f) {
            HorizontalCollisions(ref velocity);
        }

        if (velocity.y != 0f) {
            VerticalCollisions(ref velocity);
        }

        player.Translate(velocity);
    }

    public void HorizontalCollisions(ref Vector3 velocity) 
    {
        float directionX = Mathf.Sign(velocity.x);
		float rayLength = Mathf.Abs(velocity.x) + skinWidth;
		
		for (int i = 0; i < horizontalRayCount; i ++) {
			Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft :
            raycastOrigins.bottomRight;
			
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX,
            rayLength, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.white);

			if (hit) {
                Debug.Log("Horizontal hit");
                velocity.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;

                collisions.left = (directionX == -1);
                collisions.right = (directionX == 1);
			}
		}
	}

    public void VerticalCollisions(ref Vector3 velocity) 
    {
		float directionY = Mathf.Sign(velocity.y);
		float rayLength = Mathf.Abs(velocity.y) + skinWidth;

		for (int i = 0; i < verticalRayCount; i ++) {
			Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : 
                raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY,
                rayLength, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.white);

			if (hit) {
				velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                collisions.below = (directionY == -1);
                collisions.above = (directionY == 1);
			}
		}
	}

    private void MoveSprite(){
        Animator animator = player.GetComponent<Animator>();
        if(playerVelocity != Vector3.zero)
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
        
        public Vector3 velocityOld;

        public void Reset() {
            above = below = false;
            left = right = false;
        }
    }
}
