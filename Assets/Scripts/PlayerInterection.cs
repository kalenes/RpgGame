using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterection : MonoBehaviour
{
    public bool interect = false;
    private GameObject npc;
    [SerializeField] private GameObject ui;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPC")
        {
            interect = true;
            npc = other.gameObject;
            npc.GetComponent<NPCSystem>().MarkActive();
            ui.SetActive(true);
        }else
        {
            return;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPC" && npc)
        {
            interect = false;
            npc.GetComponent<NPCSystem>().DeactivateDialog();
            npc.GetComponent<NPCSystem>().MarkDeactive();
            npc = null;
            ui.SetActive(false);

        }
        else
        {
            return;
        }
    }

    public void Dialogue()
    {
        if (interect)
        {
            npc.GetComponent<NPCSystem>().ActivateDialog();
        }
        else
        {
            return;
        }
    }
}
