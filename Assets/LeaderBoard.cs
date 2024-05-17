using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
   public GameObject playerHolder;

   [Header("Options")]
   public float refreshRate = 1f;

   [Header("UI")]
   public GameObject[] slots;

   [Space]
   public TextMeshProUGUI[] scoreTexts;
   public TextMeshProUGUI[] nameTexts;

   private void Start()
   {
      InvokeRepeating(nameof(Refresh),1f,refreshRate);
   }

   public void Refresh()
   {
      foreach (var slot in slots)
      {
         slot.SetActive(false);
      }
   }
}
