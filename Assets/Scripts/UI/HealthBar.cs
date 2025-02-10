using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
   [SerializeField] private Image HealthBarImage;

   public void UpdateHealthBar(float health,float healthMax)
   {
      HealthBarImage.fillAmount = health / healthMax;
   }
}
