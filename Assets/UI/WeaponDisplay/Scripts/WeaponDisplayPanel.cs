using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
public class WeaponDisplayPanel : MonoBehaviour
{
    [Serializable]
    public class WeaponSprite
    {
        public GunType type;
        public Sprite sp;
    }
    [Header("Gun Display")]
    [SerializeField] private WeaponSprite[] weaponSprites;
    private Dictionary<GunType, Sprite> weaponMap = new Dictionary<GunType, Sprite>();
    [SerializeField] private Sprite[] sp;
    [SerializeField] private Image weaponImg;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] public LongClickHandler longClickHandler;
    [Header("Ammo Display")]
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private Slider ammoSlider;

    private void Start()
    {
        for (int i = 0; i < weaponSprites.Length; i++)
        {
            weaponMap[weaponSprites[i].type] = weaponSprites[i].sp;
        }
        UnShowGun();
    }
    public void ShowGun(GunType gunNum)
    {
        longClickHandler.enabled = true;
        weaponImg.enabled = true;
        weaponImg.sprite = weaponMap[gunNum];
    }
    public void UnShowGun()
    {
        longClickHandler.enabled = false;
        weaponImg.enabled = false;
        ammoText.text = "0<#767676>/0";
        ammoSlider.value = 0;
    }
    public void UpdateAmmo(int clipSize, int ammo, int totalAmmo)
    {
        ammoText.text = $"{ammo} <#767676>/{totalAmmo}";
        ammoSlider.value = ammo / (float)clipSize;
    }

}
