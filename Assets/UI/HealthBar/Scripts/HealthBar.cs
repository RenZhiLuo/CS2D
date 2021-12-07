using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health health;

    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text text;

    private void Start()
    {

        UpdateState();
        health.HurtHandler += OnHurt;
    }
    private void OnHurt(float damage)
    {
        UpdateState();
    }
    private void UpdateState()
    {
        slider.value = health.CurrentHealth / health.MaxHealth;
        text.text = $"{health.CurrentHealth.ToString()} / {health.MaxHealth.ToString()}";
    }
}
