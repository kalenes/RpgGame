using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeScan : MonoBehaviour
{
    public bool boss = false;
    private Transform playerTransform;
    public float distance;

    public float lookRange = 7f;
    public float attackRange = 6.4f;

    public bool canAttack = false;

    public LayerMask playerLayer;

    [SerializeField] private float lookSpeed;
    
    
    public Transform attackPoint;
    
    [SerializeField] float maxDistance =5f;
    public float radius = 2f;
    private float currentHitDistance;
    
    private Animator npcAnimator;

    private void Awake()
    {
        npcAnimator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    
    private void Update()
    {
        
        DrawSphereCast();

        
        distance = Vector3.Distance(this.transform.position, playerTransform.position);
        
        Vector3 lookDirection = playerTransform.position - transform.position;
        if (boss)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), lookSpeed * Time.deltaTime);
        }else if(!boss)
        {
            lookDirection = new Vector3(lookDirection.x, 0, lookDirection.z);
 
        }
        if (distance <= lookRange)
        {
            if (gameObject.GetComponent<EnemyTakeDamage>().inLife)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), lookSpeed * Time.deltaTime);
                npcAnimator.SetBool("Battle",true);
            }
            
        }
        else
        {
            npcAnimator.SetBool("Battle",false);
        }

        if (distance > attackRange)
        {
            canAttack = false;
        }
        
    }
    
    private void DrawSphereCast()
    {
        RaycastHit hit2;
        bool isHit =Physics.SphereCast(attackPoint.position, radius, transform.forward, out hit2,maxDistance,playerLayer);


        if (isHit)
        {
            Debug.DrawRay(attackPoint.position,transform.forward*hit2.distance,Color.red);
            currentHitDistance = hit2.distance;
            if (distance <= attackRange)
            {
                canAttack = true;
            }
        }
        else
        {
            Debug.DrawRay(attackPoint.position,transform.forward*maxDistance,Color.green);
            canAttack = false;
        }


    }

    private void OnDrawGizmosSelected()
    {
        if (canAttack)
        {
            Gizmos.color = Color.green;  
        }
        else
        {
            Gizmos.color = Color.red;
        }
        
        Debug.DrawLine(attackPoint.position,attackPoint.position + transform.forward*currentHitDistance);
        Gizmos.DrawWireSphere(attackPoint.position+ transform.forward *currentHitDistance,radius);

    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
   
}
