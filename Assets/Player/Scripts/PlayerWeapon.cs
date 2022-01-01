using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Transform weaponParent;

    [SerializeField] private WeaponDisplayPanel weaponDisplay;

    [SerializeField] private int equipParam = Animator.StringToHash("isEquipping");
    [SerializeField] private int reloadParam = Animator.StringToHash("reload");

    private Gun gun;

    private void Start()
    {
        weaponDisplay.longClickHandler.handler += DropWeapon;
        InputSystem.instance.reloadButton.onClick.AddListener(Reload);
    }
    private void OnDestroy()
    {
        weaponDisplay.longClickHandler.handler -= DropWeapon;
    }
    private void Update()
    {
        if (InputSystem.instance.IsHoldAttack)
        {
            Attack();
        }
    }
    private void Attack()
    {
        if (gun == null) return;
        gun.Shoot();
    }
    private void Reload()
    {
        if (gun == null) return;
        bool canReload =  gun.Reload();
        if (canReload) anim.SetTrigger("reload");
    }
    public void PickUpWeapon(Gun gun) 
    {
        if (this.gun) DropWeapon();

        EquipWeapon(gun);
    }
    private void EquipWeapon(Gun gun)
    {
        anim.SetBool(equipParam, true);

        this.gun = gun;
        gun.transform.SetParent(weaponParent);
        gun.transform.localPosition = Vector3.zero;
        gun.transform.localRotation = Quaternion.identity;
        gun.transform.localScale = Vector3.one;

        gun.bulletUpdateHandler += weaponDisplay.UpdateAmmo;
        weaponDisplay.ShowGun(gun.Type);

        gun.SetOnHand();
    }
    public void DropWeapon()
    {
        if (gun == null) return;

        anim.SetBool(equipParam, false);

        gun.SetOnGround();

        gun.transform.SetParent(null);
        gun.transform.rotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360));

        weaponDisplay.UnShowGun();
        gun.bulletUpdateHandler -= weaponDisplay.UpdateAmmo;

        gun = null;
    }
    

}
