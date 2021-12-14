using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class WeaponDisplayPanel : MonoBehaviour
{

    [Header("Gun Display")]
    [SerializeField] private Sprite[] sp;
    [SerializeField] private Image weaponImg;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] public LongClickHandler longClickHandler;
    [Header("Ammo Display")]
    [SerializeField] private Image ammoImg;
    [SerializeField] private TMP_Text ammoText;

    private void Start()
    {
        longClickHandler.enabled = false;
    }
    public void ShowGun(int gunNum)
    {
        longClickHandler.enabled = true;
        weaponImg.enabled = true;
        weaponImg.sprite = sp[gunNum];
    }
    public void UnShowGun()
    {
        longClickHandler.enabled = false;
        weaponImg.enabled = false;
    }
    public void UpdateAmmo(int ammo, int totalAmmo)
    {
        ammoText.text = $"{ammo} <#767676>/{totalAmmo}";
    }
}
