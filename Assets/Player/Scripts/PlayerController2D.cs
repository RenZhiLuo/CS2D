using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController2D : MonoBehaviour
{
    public static PlayerController2D instance;

    [SerializeField] private Animator anim;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float moveSpeed;
    private float InputHorizontal { get { return InputSystem.instance.Horizontal; } }
    private float InputVertical { get { return InputSystem.instance.Vertical; } }
    private bool InputRun { get { return Input.GetKey(KeyCode.Z); } }
    private bool InputJump { get { return Input.GetKeyDown(KeyCode.Space); } }

    private int walkParam = Animator.StringToHash("isWalking");
    #region Sound
    [SerializeField] private Sound footstepSound;
    [SerializeField] private float footstepInterval;
    private float footstepIntervalTimer;
    #endregion

    private void Awake()
    {
        instance = this;
    }


    private void Update()
    {

        Vector2 velocity = new Vector2(InputHorizontal, InputVertical).normalized * moveSpeed * Time.deltaTime;
        rb.position += velocity;

        //footstep sound
        if (velocity != Vector2.zero)
        {
            transform.up = velocity.normalized;
            footstepIntervalTimer += Time.deltaTime;
            if (footstepIntervalTimer >= footstepInterval)
            {
                footstepIntervalTimer = 0;
                SoundManager.instance.PlayAudio(footstepSound);
            }
        }
    }
    private void FixedUpdate()
    {
        AnimationSwitch();
    }

    private void AnimationSwitch()
    {
        anim.SetBool(walkParam, InputHorizontal != 0 || InputVertical != 0);
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
