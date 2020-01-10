using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour
{
    int life;
    int baseLife;
    int lifeRecover;
    int mana;
    int baseMana;
    int manaRecover;
    int exp;
    int nlvl;
    [SerializeField] Text health;
    [SerializeField] Text magic;
    [SerializeField] Text experience;

    // Start is called before the first frame update
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
        recover();
        health.text = life + "%";
        magic.text = mana + "%";
        experience.text = exp + "/" + nlvl + "XP";
    }

    void addExp(int addedExp)
    {
        exp += addedExp;
        if (exp > nlvl)
        {
            life = baseLife;
            mana = baseMana;
            nlvl += (int)Math.Ceiling(nlvl * 0.4);
        }
    }

    void recover()
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            //life -= other.gameObject.damage;
        }
    }
}
