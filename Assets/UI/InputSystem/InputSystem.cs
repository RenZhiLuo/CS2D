using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class InputSystem : MonoBehaviour
{
    public static InputSystem instance;
    [SerializeField] private WoXing.Joystick joystickMove;
    [SerializeField] private HoldButton attackButton;

    public Button reloadButton;
    public Button pickUpButton;

#if UNITY_EDITOR
    public float Horizontal { get { return Input.GetAxis("Horizontal"); } }
    public float Vertical { get { return Input.GetAxis("Vertical"); } }

#else
    public float Horizontal { get { return joystickMove.Horizontal; } }
    public float Vertical { get { return joystickMove.Vertical; } }
#endif
    public bool IsHoldAttack { get { return attackButton.IsHoldDown; } }
    private void Awake()
    {
        instance = this;
    }
}
