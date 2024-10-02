using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    [SerializeField] private AudioClip takeDamage;
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private AudioClip attackSound;

    private AudioSource audioSource;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayTakeDamageSound()
    {
        audioSource.clip = takeDamage;
        audioSource.Play();
    }

    public void PlayAttackSound()
    {
        audioSource.clip = attackSound;
        audioSource.Play();
    }
    
    public void PlayWalkSound()
    {
        audioSource.PlayOneShot(walkSound);
    }
}
