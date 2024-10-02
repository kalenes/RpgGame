using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
   [SerializeField] TMP_Text healthPotionCounter;
   private int healthPotNum = 0;
   
   private GameObject Player;


   private void Awake()
   {
      Player = GameObject.FindGameObjectWithTag("Player");
   }

   private void FixedUpdate()
   {
      healthPotNum = Player.GetComponent<CollectItems>().healthPotion;
      healthPotionCounter.text = healthPotNum.ToString();
   }
}
