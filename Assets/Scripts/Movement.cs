using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{ 
    [SerializeField] Transform player;
    [SerializeField] float vel;
    public LayerMask bgLayer;
    Vector3 mousePosition;
    Vector3 trueMousePosition;
    float mousePositionZ;
    bool clickedOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Move Initiate");
        player.gameObject.GetComponent<Animator>().SetBool("move", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || clickedOnce)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
                Debug.Log("Click on " + hit.collider.tag);
            
            if (hit.collider.tag == "Background")
            {
                trueMousePosition = hit.point;
                mousePosition = new Vector3(hit.point.x, hit.point.y, player.position.z);
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

        player.position = Vector3.MoveTowards(player.position, mousePosition, vel);
        Vector3 newPos = new Vector3(player.position.x, player.position.y, trueMousePosition.z - 20);
        player.position = Vector3.MoveTowards(player.position, newPos, vel);
        if (player.position.z != -1 && clickedOnce)
        {            
            player.position = new Vector3(player.position.x, player.position.y, player.position.z);
            player.position = new Vector3(player.position.x, player.position.y, trueMousePosition.z - 20);
        }

        if(player.position != new Vector3(trueMousePosition.x, trueMousePosition.y, trueMousePosition.z - 20))
        {
            player.gameObject.GetComponent<Animator>().SetBool("move", true);
        }
        else
        {
            player.gameObject.GetComponent<Animator>().SetBool("move", false);
        }
    }
}
