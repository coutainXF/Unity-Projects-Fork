using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
   [SerializeField] protected int maxHealth;
   [SerializeField] protected int curHealth;

   bool isDeath;
   public bool IsDeath => isDeath;

   public virtual void TakeDamage(int damage)
   {
      curHealth = Mathf.Clamp(curHealth-damage,0,curHealth);
   }

   public virtual void Recover(int health)
   {
      if(curHealth==maxHealth) return;
      curHealth = Mathf.Clamp(curHealth+health,curHealth,maxHealth);
   }

   protected virtual void OnEnable()
   {
      ResetHealth();
      StopAllCoroutines();
      isDeath = false;
      StartCoroutine(nameof(CheckDeath));
   }

   protected void OnDisable()
   {
      StopAllCoroutines();
   }

   public void ResetHealth()
   {
      curHealth = maxHealth;
   }

   IEnumerator CheckDeath()
   {
      while (gameObject.activeSelf)
      {
         if (curHealth <= 0)
         {
            isDeath = true;
            yield break;
         }
         yield return null;
      }
   }
}