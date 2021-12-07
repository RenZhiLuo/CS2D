using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProSlider : MonoBehaviour
{
    [SerializeField] private Button addBtn;
    [SerializeField] private Button subBtn;
    [SerializeField] private Slider slider;
    [SerializeField] private float delta;


    private void Start()
    {
        addBtn.onClick.AddListener(OnAdd);
        subBtn.onClick.AddListener(OnSub);
    }

    private void OnAdd()
    {
        slider.value += delta;
        if (slider.value > slider.maxValue)
        {
            slider.value = slider.maxValue;
        }
        slider.onValueChanged.Invoke(slider.value);
    }
    private void OnSub()
    {
        slider.value -= delta;
        if (slider.value < slider.minValue)
        {
            slider.value = slider.minValue;
        }
        slider.onValueChanged.Invoke(slider.value);
    }
}
