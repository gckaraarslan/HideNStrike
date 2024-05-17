using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    private int selectedWeapon = 0;

    public Animation animation;
    public AnimationClip weaponChange;
    void Start()
    {
        ChangeWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 1;
        }
        // if (Input.GetKeyDown(KeyCode.Alpha3))
        // {
        //     selectedWeapon = 2;  //diye gider böyle silah sayısınca
        // }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon += 1;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon -= 1;
            }
        }


        if (previousSelectedWeapon != selectedWeapon)
        {
            ChangeWeapon(); //changeweapon    
        }
    }

    void ChangeWeapon() //changeWeapon
    {
        // if (selectedWeapon>=transform.childCount)
        // {
        //     selectedWeapon = transform.childCount - 1;
        // }    //bu kod çalışmıyor
        
        animation.Stop();
        animation.Play(weaponChange.name);
        int i = 0;
        foreach (Transform _weapon in transform)
        {
            //sub objeleri geziyoruz, i ilk değer sıfır eğer selected weapon değeri mesela 2 ise o nesneyi false yapıp i yi de arttırıp bir sonraki sub objeye geçiyoruz, iyi arttırmadan geçmiyoruz
            //i oldu 1 mesela (arttırdık ve bir sonraki sub objeye geçtik ya) 1 olan i eşit değildir 2 olan selectedweapon olduğu için else koşulunu çalıştırıyor ve i'yi yine 1 arttırıyoruz ve bir sonraki sub objeye geçiyoruz...
            //i oldu 2, 2 olan i == 2 olan selectedweapon olduğu için ilk koşul çalışıyor ve o silah (o sub obje) aktif oluyor ve
            if (i == selectedWeapon)
            {
                _weapon.gameObject.SetActive(true);
            }
            else
            {
                _weapon.gameObject.SetActive(false);
            }

            i++;
        }
    }
}