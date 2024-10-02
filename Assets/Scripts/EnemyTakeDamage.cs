using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyTakeDamage : MonoBehaviour
{
    public bool inLife = true;
    public float health;
    [SerializeField] private Animator npcAnim;

    public bool count = false;
    private GameObject gameManager;


    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    public void TakeDamage(float damage)
    {
        if (health > 0 && inLife)
        {
            gameObject.GetComponent<NpcEffects>().GetHitEffect();
            health -= damage;
            DamagePopUpGenerator.current.CreatePopUp(transform.position,damage.ToString(),Color.red);
            npcAnim.SetTrigger("Get Hit Front"); 
            GetComponent<EnemySounds>().PlayTakeDamageSound();
        }
        if (health <= 0 && inLife)
        {
            Death();
            inLife = false;
        }
        
    }
    
    public void Death()
    {
        if (!count)
        {
            gameManager.GetComponent<FinalBoss>().enemyCounter++;
            count = true;
        }
        npcAnim.SetTrigger("Death");
    }
}
