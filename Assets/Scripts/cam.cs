using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject player;


    Vector3 objPosition;
    void Start()
    {

    }
    
    // Update is called once per fram
    void Update()
    {
        Vector2 ob= new Vector2(player.transform.position.x, player.transform.position.y);
        this.transform.position = ob;
    
            Debug.Log("slime :" + (player.transform.position.x.ToString())+" "+(player.transform.position.y.ToString())+" "+ player.transform.position.z.ToString());
        Debug.Log("Cam :" + (this.transform.position.x.ToString()) + " " + (this.transform.position.y.ToString()) + " " + this.transform.position.z.ToString());
    }
}
