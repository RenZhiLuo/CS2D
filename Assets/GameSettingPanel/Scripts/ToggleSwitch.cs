using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSwitch : MonoBehaviour
{

    [SerializeField] private Animator anim;
    private bool isOn;
    private void OnEnable()
    {
        anim.SetBool("isOn", isOn);
    }
    public void OnValueChanged(bool isOn)
    {
        this.isOn = isOn;
        anim.SetBool("isOn", isOn);
    }
}
