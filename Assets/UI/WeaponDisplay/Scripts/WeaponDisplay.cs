using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class WeaponDisplay : MonoBehaviour
{
    [SerializeField] private Image weaponImg;
    [SerializeField] private TMP_Text nameText;

    [SerializeField] private Sprite[] sp;

    [SerializeField] private PlayerWeapon playerWeapon;

    [SerializeField] private LongClickHandler longClickHandler;

    private void Start()
    {
        longClickHandler.enabled = false;
        playerWeapon.EquipWeaponHandler += Display;
        playerWeapon.DropWeaponHandler += Close;
    }
    public void Display(int gunNum)
    {
        longClickHandler.enabled = true;
        weaponImg.enabled = true;
        weaponImg.sprite = sp[gunNum];
    }
    public void Close()
    {
        longClickHandler.enabled = false;
        weaponImg.enabled = false;
    }
    private void OnDestroy()
    {
        playerWeapon.EquipWeaponHandler -= Display;
        playerWeapon.DropWeaponHandler -= Close;
    }

}
