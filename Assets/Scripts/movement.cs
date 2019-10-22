using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject player;
    [SerializeField] float vel;
    [SerializeField] Camera mapacamera;

    Vector3 objPosition ;
    void Start()
    {
        
    }
    void OnMouseDown()
    {
        Debug.Log("socorr");
        Vector2 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y,10);
        objPosition = new Vector3(mapacamera.ScreenToWorldPoint(mousePosition).x, mapacamera.ScreenToWorldPoint(mousePosition).y, 10);
        
    }
    // Update is called once per fram
    void Update()
    {
        player.transform.position = Vector3.MoveTowards(player.transform.position, objPosition, vel);
    }
}
