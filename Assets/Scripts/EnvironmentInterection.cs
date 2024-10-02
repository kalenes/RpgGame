using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentInterection : MonoBehaviour
{
    public GameObject ui;
    public RawImage img;
    public bool openable;
    [SerializeField] private Animator anim;
    private GameObject Player;

    private AudioSource _audioSource;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject== Player)
        {
            ui.SetActive(true);
            Player.GetComponent<PlayerController>().pickupUI.SetActive(true);
            if (Player.GetComponent<CollectItems>().key)
            {
                img.color = Color.white;
                openable = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {   
            Player.GetComponent<PlayerController>().pickupUI.SetActive(false);
            ui.SetActive(false);
            openable = false;
        }
    }

    public void OpenDoor(bool key)
    {
        if (key && openable)
        {           
            ui.SetActive(false);
            _audioSource.Play();
            anim.SetBool("Open",true);
        }
    }
}
