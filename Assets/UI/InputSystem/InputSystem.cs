using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InputSystem : MonoBehaviour
{
    public static InputSystem instance;
    [SerializeField] private WoXing.Joystick joystickMove;
    public Button attackButton;

#if UNITY_EDITOR
    public float Horizontal { get { return Input.GetAxis("Horizontal"); } }
    public float Vertical { get { return Input.GetAxis("Vertical"); } }

#else
    public float Horizontal { get { return joystickMove.Horizontal; } }
    public float Vertical { get { return joystickMove.Vertical; } }
#endif

    public float AttackHorizontal { get { return joystickMove.Horizontal; } }
    public float AttackVertical { get { return joystickMove.Vertical; } }
    private void Awake()
    {
        instance = this;
    }
    private void OnDestroy()
    {
        instance = null;
    }
}
