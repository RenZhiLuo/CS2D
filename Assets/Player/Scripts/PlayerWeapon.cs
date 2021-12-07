using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Transform weaponParent;

    public event Action<int> EquipWeaponHandler;
    public event Action DropWeaponHandler;

    private Gun gun;
    private void Start()
    {
        InputSystem.instance.attackButton.onClick.AddListener(Attack);
    }

    private void Attack()
    {
        if (gun == null) return;
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
        this.gun = gun;
        gun.transform.SetParent(weaponParent);
        gun.transform.localPosition = Vector3.zero;
        gun.transform.localRotation = Quaternion.identity;
        gun.transform.localScale = Vector3.one;

        EquipWeaponHandler?.Invoke((int)gun.GunType);
    }
    public void DropWeapon()
    {
        if (gun == null) return;
        gun.transform.SetParent(null);
        gun = null;

        DropWeaponHandler?.Invoke();
    }

}
