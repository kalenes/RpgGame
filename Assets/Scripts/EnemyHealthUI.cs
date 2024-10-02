using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    private float health;
    private float currentHealth;
    private bool inLife = true;
    
    private Transform player;
    public Slider healthBar;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        health = gameObject.GetComponent<EnemyTakeDamage>().health;
        currentHealth = health;
    }

    private void Update()
    {
        if (!inLife)
        {
            healthBar.gameObject.SetActive(false); 
            gameObject.GetComponent<DropItem>().Drop();
            StartCoroutine(Destroy());
        }
        
        inLife =  gameObject.GetComponent<EnemyTakeDamage>().inLife;

        currentHealth = gameObject.GetComponent<EnemyTakeDamage>().health;
        healthBar.value = (currentHealth / health) * 100f;
    }
    
    public IEnumerator Destroy()
    {
        yield return  new WaitForSeconds(3.5f);

        gameObject.SetActive(false);
    }
}
