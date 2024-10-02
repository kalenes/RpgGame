using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public int rockCounter = 0;
    public int enemyCounter=0;
    private bool rockActive = false;
    
    public GameObject boss;
    public Transform CameraHolder;
    Quaternion newRotation;
    private Vector3 targetPos;
    public float targetY;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip music1;
    [SerializeField] private AudioClip music2;
    private bool fight = false;

    [SerializeField] private GameObject[] magicRocks;
    private void Awake()
    {
        targetPos = new Vector3(boss.transform.position.x, targetY, boss.transform.position.z);
        newRotation = Quaternion.Euler(22, 0, 0);
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (enemyCounter == 3)
        {
            if (!fight)
            {
                _audioSource.clip = music2;
                _audioSource.Play();
                fight = true;
            }
            
            CameraHolder.rotation = Quaternion.Lerp(CameraHolder.rotation,newRotation,Time.deltaTime * 10);

            boss.transform.position = Vector3.MoveTowards(boss.transform.position, targetPos, 10 * Time.deltaTime);
            boss.GetComponent<BigEyeBoss>().canAttack = true;
            for (int i = 0; i < 3; i++)
            {
                if (rockActive == false)
                {
                    magicRocks[i].SetActive(true);
                }
            }

            rockActive = true;
        }

        if (rockCounter == 3 && boss.GetComponent<BigEyeBoss>().life)
        {
            boss.GetComponent<BigEyeBoss>().Death();
            if (fight)
            {
                _audioSource.clip = music2;
                _audioSource.Play();
                fight = false;
            }

        }
    }
}
