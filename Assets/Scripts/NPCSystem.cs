using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class NPCSystem : MonoBehaviour
{
    public GameObject UI;
    public GameObject Mark;

    private bool welcome = true;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip welcomeSound;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void ActivateDialog()
    {
        if (welcome)
        {
            _audioSource.Play();
            welcome = false;
        }

        UI.SetActive(true);
    }

    public void DeactivateDialog()
    {
        UI.SetActive(false);
        _audioSource.clip = welcomeSound;
        welcome = true;
    }

    public void StartDungeon(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);

    }

    public void MarkActive()
    {
        Mark.SetActive(true);
    }
    public void MarkDeactive()
    {
        Mark.SetActive(false);
    }
}
