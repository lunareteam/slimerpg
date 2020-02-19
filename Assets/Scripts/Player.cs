using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables
    private int life;
    private int baseLife;
    private int lifeRecover;
    private int mana;
    private int baseMana;
    private int manaRecover;
    private int exp;
    private int nlvl;

    // Start is called when object is spawned
    void Start()
    {
        this.GetComponent<Animator>().SetBool("move", false);
        baseLife = 100;
        life = baseLife;
        lifeRecover = 1;
        mana = 100;
        baseMana = 100;
        manaRecover = 10;
        exp = 1;
        nlvl = 100;
    }

    // Update is called once per frame
    void Update()
    {
        Recover();
    }
    
    // Triggered
    void OnTriggerEnter(Collider other)
    {
        // If collides with enemy, life goes down by damage from enemy
        if (other.gameObject.tag == "enemy")
        {
            life -= other.gameObject.GetComponent<Enemy>().GetDamage();
        }
    }

    // Functions
    void Recover()
    {
        if (life < baseLife)
        {
            if (life + lifeRecover > baseLife)
            {
                life = baseLife;
            }
            else
            {
                life += lifeRecover;
            }
        }
        if (mana < baseMana)
        {
            if (mana + manaRecover > baseMana)
            {
                mana = baseMana;
            }
            else
            {
                mana += manaRecover;
            }
        }
    }

    // Getters
    public int GetLife()
    {
        return life;
    }

    public int GetMana()
    {
        return mana;
    }

    public int GetExp()
    {
        return exp;
    }

    public int GetNLVL()
    {
        return nlvl;
    }

    // Setters
    public void AddExp(int addedExp)
    {
        // Should show gained exp on screen
        /**** Not Implemented ****/
        // Add exp to player
        exp += addedExp;
        if (exp >= nlvl)
        {
            // Base param
            baseLife += (int)Math.Ceiling(baseLife*0.2);
            baseMana += (int)Math.Ceiling(baseMana*0.2);

            // Recover params
            life = baseLife;
            mana = baseMana;

            // New lvlup requirement
            nlvl += (int)Math.Ceiling(nlvl * 0.4);
        }
    }
}
