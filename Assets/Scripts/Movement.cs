using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{ 
    [SerializeField] GameObject player;
    [SerializeField] float vel;
    public LayerMask bgLayer;
    Vector3 mousePosition;
    bool clickedOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Move Initiate");
        player.GetComponent<Animator>().SetBool("move", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || clickedOnce)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            Debug.Log("Click on " + hit.collider.tag);
            
            if (hit.collider.tag == "Background")
            {
                mousePosition = hit.point;
                clickedOnce = true;
            }
            else
            {
                clickedOnce = false;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            clickedOnce = false;
        }

        player.GetComponent<Transform>().position = Vector3.MoveTowards(player.GetComponent<Transform>().position, mousePosition, vel);
        if (player.GetComponent<Transform>().position.z != -1 && clickedOnce)
        {            
            player.GetComponent<Transform>().position = new Vector3(player.GetComponent<Transform>().position.x, player.GetComponent<Transform>().position.y, 1);
        }

        if(player.GetComponent<Transform>().position != mousePosition)
        {
            player.GetComponent<Animator>().SetBool("move", true);
        }
        else
        {
            player.GetComponent<Animator>().SetBool("move", false);
        }
    }
}
