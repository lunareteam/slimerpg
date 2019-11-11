using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class networksync : NetworkBehaviour
{   GameObject touch;
    // Start is called before the first frame update
    GameObject[] path;
    GameObject[] players;
    GameObject Player;
    Animator animator;
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        path = GameObject.FindGameObjectsWithTag("path");
        touch = path[0];
        foreach(GameObject i in players)
        {
            if (i.transform.position != touch.GetComponent<movement>().getplayer().position)
            {
                Player = i;
                break;
            }
        }

        animator = Player.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            this.transform.position = touch.GetComponent<movement>().getplayer().position;
            Player.GetComponent<Animator>().SetBool("local", false);
        }
        else
        {
            Player.GetComponent<Animator>().SetBool("local", true);
        }
        Debug.Log(Player.GetComponent<Animator>().GetBool("local"));
    }
}
