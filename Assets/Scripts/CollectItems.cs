using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItems : MonoBehaviour
{
    private GameObject item;
    [SerializeField] private GameObject pickupUI;

    public bool key;
    public int healthPotion = 3;
    public int healthPotValue = 25;

    private float collectTimer =0f;
    private float collectTime = 0.5f;

    private float potionCooldown = 1f;

    private void Update()
    {
        if (collectTimer >=0)
        {
            collectTimer -= Time.deltaTime;
        }

        if (potionCooldown < 1)
        {
            potionCooldown += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Collectable")
        {
            pickupUI.SetActive(true);
            item = col.gameObject;
        }

    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Collectable")
        {
            pickupUI.SetActive(false);
            item = null;
        }
    }
    
    public void PickUpItem()
    {
        if (item)
        {
            item.gameObject.SetActive(false);
            pickupUI.SetActive(false);
            if (item.name == "Health Potion")
            {
                if (collectTimer <=0)
                {
                    collectTimer = collectTime;
                    healthPotion+=1;
                    item= null;
                }
            }else if (item.name == "Key")
            {
                key = true;
                gameObject.GetComponent<PlayerController>().key = true;
                item= null;
            }
        }
    }

    public void DrinkHealthPot()
    {
        
        if (healthPotion > 0 && potionCooldown >= 1)
        {
            if (gameObject.GetComponent<PlayerController>().currentHealth > 0 && gameObject.GetComponent<PlayerController>().currentHealth < 100 )
            {
                potionCooldown = 0;
                gameObject.GetComponent<EffectController>().PlayHealEffect();
                GetComponent<PlayerSounds>().HealingSound();
                healthPotion--;
                gameObject.GetComponent<PlayerController>().currentHealth += healthPotValue;
                if (gameObject.GetComponent<PlayerController>().currentHealth > 100)
                {
                    gameObject.GetComponent<PlayerController>().currentHealth = 100;
                }
            }
            
            
        }
    }
}
