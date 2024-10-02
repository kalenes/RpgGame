using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicRock : MonoBehaviour
{
    private float currentHealth;
    public bool inLife = true;
    public float health;
    
    public Slider healthBar;

    
    private GameObject gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        currentHealth = health;
    }

    private void Update()
    {
        if (inLife)
        {
            healthBar.value = (health / currentHealth) * 100f;
            
            if (health <= 0)
            {       
                Death();
                inLife = false;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (health > 0)
        {  
            gameObject.GetComponent<NpcEffects>().GetHitEffect();

            health -= damage;
            
        }
        
    }
    
    public void Death()
    {
        gameObject.SetActive(false);
        healthBar.gameObject.SetActive(false);
        gameManager.GetComponent<FinalBoss>().rockCounter++;
    }
    
}
