using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageBox : MonoBehaviour
{
    
    [SerializeField] private float damage;
    [SerializeField] private float bonusDamage;

    private float currentDamage;
    
    private bool trail =false;

    private GameObject Player;

    private void Awake()
    {
        currentDamage = damage;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        trail = Player.GetComponent<PlayerController>().swordTrail;
        
        if (trail)
        {
            currentDamage += bonusDamage;
        }
        else if (!trail)
        {
            currentDamage = damage;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") 
        {
                other.gameObject.GetComponent<EnemyTakeDamage>().TakeDamage(currentDamage);
        }
        if (other.gameObject.tag == "Stone") 
        {
            other.gameObject.GetComponent<MagicRock>().TakeDamage(currentDamage);
        }
    }
}
