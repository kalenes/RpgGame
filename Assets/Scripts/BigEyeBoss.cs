using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEyeBoss : MonoBehaviour
{
    private float coolDownTimer = 0;
    public bool canAttack = false;

    [SerializeField] private float cooldown;

    [SerializeField] private GameObject[] attackPoints;
    [SerializeField] private GameObject[] bullets;
    public bool life = true;
    private GameObject player;
    private Transform playerPos;

    private Vector3 dir;
    
    [SerializeField] private Animator npcAnim;

    public float bulletSpeed =10f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        coolDownTimer = cooldown;
        npcAnim.SetBool("Battle",true);

    }

    private void Update()
    {
        playerPos = player.transform;

        
        if (canAttack)
        {
            if (coolDownTimer >0)
            {
                coolDownTimer -= Time.deltaTime;
            }
            if (coolDownTimer <=0)
            {
                attack(playerPos);
            }
        }
        
        
    }
    
    public void attack(Transform playerP)
    {
        for (int i = 0; i < 6; i++)
        {
            dir = playerP.position - attackPoints[i].transform.position;
            dir = dir.normalized;

            //coolDownTimer = coolDown;
            npcAnim.SetTrigger("attack");

            bullets[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            bullets[i].transform.position = attackPoints[i].transform.position;
            bullets[i].SetActive(true);
            //bulletDestroyTimer = bulletDestroyTime;
            bullets[i].GetComponent<Rigidbody>().AddForce(dir * bulletSpeed * 60);
        }

        coolDownTimer = cooldown;

    }
    
    public void Death()
    {
            npcAnim.SetTrigger("Death");
            life = false;
            canAttack = false;
            StartCoroutine(Destroy());
    }
    
    public IEnumerator Destroy()
    {
        yield return  new WaitForSeconds(1f);

        gameObject.SetActive(false);
    }
}

