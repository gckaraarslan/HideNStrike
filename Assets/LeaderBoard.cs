using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
   [FormerlySerializedAs("playerHolder")] public GameObject playersHolder;

   [Header("Options")]
   public float refreshRate = 1f;

   [FormerlySerializedAs("slots")]
   [Header("UI")]
   public GameObject[] playerSlots;

   [Space]
   public TextMeshProUGUI[] scoreTexts;
   public TextMeshProUGUI[] nameTexts;

   private void Start()
   {
      InvokeRepeating(nameof(Refresh),1f,refreshRate);
   }

   public void Refresh()
   {
      foreach (var slot in playerSlots)
      {
         slot.SetActive(false);
      }

      var sortedPlayerList =
         (from player in PhotonNetwork.PlayerList orderby player.GetScore() descending select player).ToList();

      int i = 0;
      foreach (var player in sortedPlayerList)
      {
         playerSlots[i].SetActive(true);
         if (player.NickName=="")
         {
            player.NickName = "unnamed";
         }

         nameTexts[i].text = player.NickName;
         scoreTexts[i].text = player.GetScore().ToString();

         i++;
      }
   }

   private void Update()
   {
      playersHolder.SetActive(Input.GetKey(KeyCode.Tab));
   }
}
