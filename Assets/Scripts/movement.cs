using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject player;
    [SerializeField] float vel;
    [SerializeField] Camera mapacamera;


    Vector3 fa ;

    void Start()
    {
        
    }

    void OnMouseDrag()
    {
        Vector2 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y,10);
        fa = new Vector3(mapacamera.ScreenToWorldPoint(mousePosition).x, mapacamera.ScreenToWorldPoint(mousePosition).y, 10);
        
    }
    void OnMouseClick()
    {
        Vector2 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y,10);
        fa = new Vector3(mapacamera.ScreenToWorldPoint(mousePosition).x, mapacamera.ScreenToWorldPoint(mousePosition).y, 10);

        
    }
    // Update is called once per fram
    void Update()
    {

        player.transform.position = Vector3.MoveTowards(player.transform.position, fa, vel);
        if (player.transform.position.z != 10)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 10); 
        }

    }
}
