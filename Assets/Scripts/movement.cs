using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float vel;
    Vector3 mousePosition;
    bool clickedOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Move Initiate");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || clickedOnce)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10f));
            clickedOnce = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            clickedOnce = false;
        }

        player.position = Vector3.MoveTowards(player.position, mousePosition, vel);
        if (player.position.z != -10 && clickedOnce)
        {
            player.position = new Vector3(player.position.x, player.position.y, 10);
        }     
    }
}
