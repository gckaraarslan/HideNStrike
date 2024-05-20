using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
 public int damage;
 public Camera camera;
 public float firePerSeconds; //fire rate 
 private float nextFireMoment;

 [Header("Ammo")] public int mag = 5;

 public int ammo = 30;
 public int magAmmo = 30;

 [Header("UI")] public TextMeshProUGUI magText;

 public TextMeshProUGUI ammoText;

 [Header("Animation")] 
 public Animation animation;

 public AnimationClip reload;

 [Header("Recoil Settings")]
 // [Range(0, 1)]
 // public float recoilPercent = 0.3f;

 [Range(0, 2)] public float recoverPercent = 0.7f;

 [Space] 
 public float recoilUp = 1f;
 public float recoilBack = 0f;

 private Vector3 originalPosition;
 private Vector3 recoilVelocity = Vector3.zero;

 private float recoilLength;
 private float recoverLength;

 private bool recoiling;
 public bool recovering;
private void Start()
 {
  magText.text = mag.ToString();
  ammoText.text = ammo + "/" + magAmmo;

  originalPosition = transform.localPosition;

  recoilLength = 0;
  recoverLength = 1 / firePerSeconds * recoverPercent;
  
 }

 private void Update()
 {
 
  
  if (nextFireMoment>0)
  {
   nextFireMoment -= Time.deltaTime;
  }

  if (Input.GetButton("Fire1") && nextFireMoment<=0 && ammo>0 && animation.isPlaying==false )
  {
   nextFireMoment = 1 / firePerSeconds;
   ammo--;
   magText.text = mag.ToString();
   ammoText.text = ammo + "/" + magAmmo;
   Fire();
   
  }

  if (Input.GetKeyDown(KeyCode.R) && mag>0)
  {
   Reload();
  }

  if (recoiling)
  {
   Recoil();
  }

  if (recovering)
  {
   Recover();
  }
 }

 private void Reload()
 {
  animation.Play(reload.name);
  if (mag>0)
  {
   mag--;
   ammo = magAmmo;
  }
  magText.text = mag.ToString();
  ammoText.text = ammo + "/" + magAmmo;
 }

 private void Fire()
 {
  recoiling = true;
  recovering = false;
  Ray ray = new Ray(camera.transform.position, camera.transform.forward);
  RaycastHit hit;
  //Debug.DrawRay(ray.origin,ray.direction);
  if (Physics.Raycast(ray.origin,ray.direction,out hit,100f))
  {
   //PhotonNetwork.LocalPlayer.AddScore(1);
   if (hit.transform.gameObject.GetComponent<Health>()) //HEALTH SCRIPTINI TAŞIYANLARA VURDUĞUNDA...
   {
    PhotonNetwork.LocalPlayer.AddScore(25);
    if (damage>=hit.transform.gameObject.GetComponent<Health>().health) //VEYA DAMAGE >HEALTH //TODO:AMA BİR SORUN VAR BURDA...
    {
     Debug.Log("HASAR VERİLDİ Mİ ???"); //BU DA ÇALIŞMADI ÇÜNKÜ IF KOŞULU ÇALIŞMIYOR...
     RoomManager.instance.kills++;
     RoomManager.instance.SetHashes();
     
     PhotonNetwork.LocalPlayer.AddScore(100);
     
    }
    hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage",RpcTarget.All,damage);
   }
  }
 }

 private void Recoil()
 {
  Vector3 finalPosition =
   new Vector3(originalPosition.x, originalPosition.y + recoilUp, originalPosition.z - recoilBack);

  transform.localPosition =
   Vector3.SmoothDamp(transform.localPosition, finalPosition, ref recoilVelocity, recoilLength);

  if (transform.localPosition==finalPosition)
  {
   recoiling = false;
   recovering = true;
  }
 }
 
 
 private void Recover()
 {
  Vector3 finalPosition = originalPosition;
   

  transform.localPosition =
   Vector3.SmoothDamp(transform.localPosition, finalPosition, ref recoilVelocity, recoverLength);

  if (transform.localPosition==finalPosition)
  {
   recoiling = false;
   recovering = false;
  }
 }
}
