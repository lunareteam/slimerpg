using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    public LayerMask collisionMask;

    public const float skinWidth = .015f;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    [HideInInspector]
    public float horizontalRaySpacing;
    [HideInInspector]
    public float verticalRaySpacing;

    public Transform player;
    public BoxCollider2D playerCollider;
    public RaycastOrigins raycastOrigins;

    public virtual void Awake()
    {
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
        player = playerCollider.GetComponent<Transform>();
        CalculateRaySpacing();
    }

    public struct RaycastOrigins {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
    
    public void UpdateRaycastOrigins()
    {
        Bounds bounds = playerCollider.bounds;
        bounds.Expand(skinWidth * -2);
        
        raycastOrigins.bottomLeft = new Vector2(player.position.x - bounds.extents.x,
        player.position.y - bounds.extents.y);
        raycastOrigins.bottomRight = new Vector2(player.position.x + bounds.extents.x,
        player.position.y - bounds.extents.y);
        raycastOrigins.topLeft = new Vector2(player.position.x - bounds.extents.x,
        player.position.y + bounds.extents.y);
        raycastOrigins.topRight = new Vector2(player.position.x + bounds.extents.x,
        player.position.y + bounds.extents.y);
    }

    public void CalculateRaySpacing()
    {
        Bounds bounds = playerCollider.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }
}
