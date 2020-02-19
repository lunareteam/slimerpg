using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int damage;
    private int life;
    private int droppedExp;
    [SerializeField] Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(life<=0){
            // It is necessary to insert death animation below

            // Add exp to player
            player.AddExp(droppedExp);
            // Destroy instance
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // If collides with enemy, life goes down by damage from enemy
        if (other.gameObject.tag == "player")
        {
            // Does not make much sense
            // life -= other.gameObject.GetComponent<Skills>().getDamage();
        }
    }

    public int GetDamage(){
        return damage;
    }
}
