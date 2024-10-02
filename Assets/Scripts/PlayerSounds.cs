using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] takeDamage;
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private AudioClip healing;
    private AudioSource audioSource;
    
    
    [SerializeField] private AudioClip[] attacksClips;
    

    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDamageSound(int i)
    {
        audioSource.clip = takeDamage[i];
        audioSource.Play();
    }

    public void WalkSound()
    {
        audioSource.PlayOneShot(walkSound);
    }
    
    public void AttackSound(int i)
    {
        audioSource.clip = attacksClips[i];
        audioSource.Play();
    }

    public void HealingSound()
    {
        audioSource.clip = healing;
        audioSource.Play();  
    }
}
