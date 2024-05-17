using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
   public Movement movement;
   public GameObject camera;
   public string nickname;
   public TextMeshPro playerNickName;

   public void IsLocalPlayer()
   {
      movement.enabled = true;
      camera.SetActive(true);
   }

   [PunRPC]
   public void SetNickname(string _name)
   {
      nickname = _name;
      playerNickName.text = nickname;
   }
}
