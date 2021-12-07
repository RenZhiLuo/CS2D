using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public static PlayerController2D instance;

    [SerializeField] private Animator anim;

    [SerializeField] private float moveSpeed;
    private float InputHorizontal { get { return InputSystem.instance.Horizontal; } }
    private float InputVertical { get { return InputSystem.instance.Vertical; } }
    private bool InputRun { get { return Input.GetKey(KeyCode.Z); } }
    private bool InputJump { get { return Input.GetKeyDown(KeyCode.Space); } }

    private bool isFacingRight;
    public bool IsFacingRight { get { return isFacingRight; } }

    #region Sound
    [SerializeField] private float footstepInterval;
    private float footstepIntervalTimer;
    #endregion

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (InputHorizontal > 0)
        {
            isFacingRight = true;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (InputHorizontal < 0)
        {
            isFacingRight = false;
            transform.localScale = new Vector3(1, 1, 1);
        }

        Vector2 velocity = new Vector2(InputHorizontal, InputVertical) * moveSpeed * Time.deltaTime;
        transform.position += (Vector3)velocity;

        //footstep sound
        if (velocity != Vector2.zero)
        {
            transform.up = velocity;
            footstepIntervalTimer += Time.deltaTime;
            if (footstepIntervalTimer >= footstepInterval)
            {
                footstepIntervalTimer = 0;
                SoundManager.instance.PlayAudio(ClipType.Footstep);
            }
        }
    }
    private void FixedUpdate()
    {
        AnimationSwitch();
    }

    private void AnimationSwitch()
    {
        if (InputHorizontal != 0)
        {
            anim.SetFloat("InputX", InputHorizontal);

        }
        if (InputVertical != 0)
        {
            anim.SetFloat("InputY", InputVertical);
        }

    }

    private void OnDestroy()
    {
        instance = null;
    }
}
