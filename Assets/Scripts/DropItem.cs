using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private GameObject Item;
    [SerializeField] private bool drop;
    
    public void Drop()
    {
        if (drop)
        {
            Item.transform.position = transform.position;
            Item.SetActive(true);
            drop = false;
        }
    }
}
