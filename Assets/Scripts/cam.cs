using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject player;
    GameObject mapa;
    GameObject[] path;
    Vector3 objPosition;
    
    void Start()
    { 
        path = GameObject.FindGameObjectsWithTag("path");
    }
  
 
    // Update is called once per fram
    void Update()
    {

        bool flagx=true, flagy=true;
        float oldx = this.transform.position.x;
        float oldy = this.transform.position.y;
        Vector2 ob= new Vector2(player.transform.position.x, player.transform.position.y);
        this.transform.position = ob;
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        foreach (GameObject mapa in path) {    
            if (!(((this.transform.localPosition.x + (width / 2)) > mapa.transform.position.x + mapa.GetComponent<Collider2D>().bounds.size.x / 2) || ((this.transform.localPosition.x - (width / 2)) < mapa.transform.position.x - mapa.GetComponent<Collider2D>().bounds.size.x / 2)))
            {
                flagx = false;
            }
            if (!(((this.transform.localPosition.y + (height / 2)) > mapa.transform.position.y + mapa.GetComponent<Collider2D>().bounds.size.y / 2) || ((this.transform.localPosition.y - (height / 2)) < mapa.transform.position.y - mapa.GetComponent<Collider2D>().bounds.size.y / 2)))
            {
                flagy = false;
            }
        } 
        if(flagx)
            this.transform.position = new Vector2(oldx, this.transform.position.y);
        if(flagy)
            this.transform.position = new Vector2(this.transform.position.x, oldy);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);
    }
}
