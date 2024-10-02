using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyNPC : MonoBehaviour
{
    private float health = 100f;
    private float currentHealth;
    
    private bool inLife = true;
    
    [SerializeField] private Animator npcAnim;
    private Transform player;
    public Slider healthBar;
    private NavMeshAgent nMesh;
    
    [SerializeField] private GameObject damageBox;

    public float attackSpeed;
    private float attackTimer;
    private int attackCount = 0;
    
    public float followRange;
    public float attackRange;
    public float range;
    private void Start()
    {
        nMesh = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        attackTimer = 0f;
        
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
        

        if (inLife)
        {
            range = Vector3.Distance(this.transform.position, player.position);
            if (range <= followRange && range > attackRange)
            {
                npcAnim.SetBool("Combat Idle",true);
                nMesh.isStopped = false;
                nMesh.SetDestination(player.position);
                npcAnim.SetBool("Run Forward",true);
            }else if (range <= attackRange)
            {
                if (player.gameObject.GetComponent<PlayerController>().inLife)
                {
                    transform.LookAt(player);
                    nMesh.isStopped = true;
                    npcAnim.SetBool("Run Forward",false);

                    npcAnim.SetTrigger("Stop");
                    if (attackTimer <= 0)
                    {
                        Attack();
                        attackCount++;

                    } 
                }
                
            } 
            else
            {
                nMesh.isStopped = true;
                npcAnim.SetBool("Run Forward",false);
                npcAnim.SetTrigger("Stop");
            }
            
            
        }

        inLife =  gameObject.GetComponent<EnemyTakeDamage>().inLife;

        //nMesh.destination = player.position;
        attackTimer -= Time.deltaTime;
        currentHealth = gameObject.GetComponent<EnemyTakeDamage>().health;
        healthBar.value = (currentHealth / health) * 100f;
    }


    

    private void Attack()
    {
        attackTimer = attackSpeed;
        if (attackCount == 1)
        {
            npcAnim.SetTrigger("Attack1");
        }
        if (attackCount == 2)
        {
            npcAnim.SetTrigger("Attack2");

        }
        if (attackCount == 3)
        {
            npcAnim.SetTrigger("Attack3");

        }
        if (attackCount == 4)
        {
            npcAnim.SetTrigger("Attack5");

            attackCount = 0;
        }
        npcAnim.SetBool("Run Forward",false);
        
    }
    
    
    public IEnumerator Damage()
    {
        damageBox.SetActive(true);
        yield return  new WaitForSeconds(1);
        damageBox.SetActive(false);
    }
    
    public IEnumerator Destroy()
    {
        yield return  new WaitForSeconds(3.5f);

        gameObject.SetActive(false);
    }

}
