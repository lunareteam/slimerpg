using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{ 
    [SerializeField] GameObject player;
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
        player.GetComponent<Animator>().SetBool("move", false);
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
                mousePosition = new Vector3(hit.point.x, hit.point.y, player.GetComponent<Transform>().position.z);
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
        Vector3 newPos = new Vector3(player.GetComponent<Transform>().position.x, player.GetComponent<Transform>().position.y, trueMousePosition.z - 20);
        player.GetComponent<Transform>().position = Vector3.MoveTowards(player.GetComponent<Transform>().position, newPos, vel);
        if (player.GetComponent<Transform>().position.z != -1 && clickedOnce)
        {            
            player.GetComponent<Transform>().position = new Vector3(player.GetComponent<Transform>().position.x, player.GetComponent<Transform>().position.y, player.GetComponent<Transform>().position.z);
            player.GetComponent<Transform>().position = new Vector3(player.GetComponent<Transform>().position.x, player.GetComponent<Transform>().position.y, trueMousePosition.z - 20);
        }

        if(player.GetComponent<Transform>().position != new Vector3(trueMousePosition.x, trueMousePosition.y, trueMousePosition.z - 20))
        {
            player.GetComponent<Animator>().SetBool("move", true);
        }
        else
        {
            player.GetComponent<Animator>().SetBool("move", false);
        }
    }
}
