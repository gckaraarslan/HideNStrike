using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
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
private void Start()
 {
  magText.text = mag.ToString();
  ammoText.text = ammo + "/" + magAmmo;
 }

 private void Update()
 {
  if (nextFireMoment>0)
  {
   nextFireMoment -= Time.deltaTime;
  }

  if (Input.GetButton("Fire1") && nextFireMoment<=0 && ammo>0 && animation.isPlaying==false)
  {
   nextFireMoment = 1 / firePerSeconds;
   ammo--;
   magText.text = mag.ToString();
   ammoText.text = ammo + "/" + magAmmo;
   Fire();
   
  }

  if (Input.GetKeyDown(KeyCode.R))
  {
   Reload();
  }
 }

 void Reload()
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

 void Fire()
 {

  Ray ray = new Ray(camera.transform.position, camera.transform.forward);
  RaycastHit hit;
  //Debug.DrawRay(ray.origin,ray.direction);
  if (Physics.Raycast(ray.origin,ray.direction,out hit,100f))
  {
   if (hit.transform.gameObject.GetComponent<Health>())
   {
    hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage",RpcTarget.All,damage);
   }
  }
 }
}
