using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RangedAttack : MonoBehaviour
{
    private bool canAttack = false;
    [SerializeField] private float coolDown;
    private float coolDownTimer = 0;
    
    public Transform attackPoint;
    public GameObject bullet;
    public float bulletSpeed = 1f;
    private Vector3 v3Force;

    [SerializeField] private float bulletDestroyTime;
    private float bulletDestroyTimer = 0;

    private Animator npcAnimator;

    private void Awake()
    {
        npcAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        canAttack = gameObject.GetComponent<RangeScan>().canAttack;
        
        v3Force = bulletSpeed * gameObject.transform.forward;

        if (coolDownTimer >0)
        {
            coolDownTimer -= Time.deltaTime;
        }
        if (canAttack && coolDownTimer <=0)
        {
            npcAnimator.SetTrigger("attack");
        }
        if (bulletDestroyTimer > 0)
        {
            bulletDestroyTimer -= Time.deltaTime;
        }else if (bulletDestroyTimer <= 0)
        {
            bullet.SetActive(false);
        }
    }

    public void attack()
    {
        coolDownTimer = coolDown;
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bullet.transform.position = attackPoint.position;
        bullet.SetActive(true);
        bulletDestroyTimer = bulletDestroyTime;
        bullet.GetComponent<Rigidbody>().AddForce(v3Force*100f);
    }
}
