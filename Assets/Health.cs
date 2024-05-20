using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    
    //TODO:health<=0 olduğunda bir event fırlatıcaz score dinliyecek, ui dinleyecek, gerekli olan herkes dinleyecek...
    public int health;
   public bool isLocalPlayer;

    [Header("UI")] public TextMeshProUGUI healthText;

    [PunRPC]
    public void TakeDamage(int _damage)
    {
        health -= _damage;
        healthText.text = health.ToString();
        if (health <= 0)
        {
            
          
            if (isLocalPlayer) 
            {
                RoomManager.instance.SpawnPlayer();
                RoomManager.instance.deaths++;
                RoomManager.instance.SetHashes();
            }
            
           
            
            Destroy(gameObject);    //TODO:destroy değil de setactive false yapıcaz... object pool...

           
        }
    }

   

    
}