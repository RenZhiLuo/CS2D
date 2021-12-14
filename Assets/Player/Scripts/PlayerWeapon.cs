using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Transform weaponParent;

    [SerializeField] private WeaponDisplayPanel display;

    private Gun gun;

    private void Start()
    {
        display.longClickHandler.handler += DropWeapon;
    }
    private void OnDestroy()
    {
        display.longClickHandler.handler -= DropWeapon;
    }
    private void Update()
    {
        if (InputSystem.instance.IsHoldAttack)
        {
            if (gun != null)
                Attack();
        }
    }
    private void Attack()
    {
        gun.Fire();
    }
    public void PickUpWeapon(Collider2D coll) 
    {
        if (this.gun) return;
        if (coll.TryGetComponent<Gun>(out Gun gun))
        {
            EquipWeapon(gun);
        }
    }
    private void EquipWeapon(Gun gun)
    {
        anim.SetBool("isEquip", true);

        this.gun = gun;
        gun.transform.SetParent(weaponParent);
        gun.transform.localPosition = Vector3.zero;
        gun.transform.localRotation = Quaternion.identity;
        gun.transform.localScale = Vector3.one;

        gun.SetOnHand();

        gun.bulletUpdateHandler += display.UpdateAmmo;
        display.ShowGun((int)gun.Type);
    }
    public void DropWeapon()
    {
        if (gun == null) return;

        anim.SetBool("isEquip", false);

        gun.SetOnGround();

        gun.transform.SetParent(null);


        display.UnShowGun();
        gun.bulletUpdateHandler -= display.UpdateAmmo;

        gun = null;
    }
    

}
