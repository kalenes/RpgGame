using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Debuffs : MonoBehaviour
{
    public bool burning = false;
    public float burningTime = 3f;
    private float second = 0f;
    [SerializeField] private GameObject burnEffect;


    private void Update()
    {
        if (burning)
        {
            burningTime -= Time.deltaTime;
            second += Time.deltaTime;
            if (second >= 0.5f)
            {
                Burn();
                second = 0;
            }

            if (burningTime <= 0)
            {
                burning = false;
                burningTime = 3f;
                burnEffect.SetActive(false);
            }
            
        }
    }

    private void Burn()
    {
        burnEffect.SetActive(true);
        gameObject.GetComponent<PlayerController>().TakeDamage(Random.Range(3,7));
    }
}
