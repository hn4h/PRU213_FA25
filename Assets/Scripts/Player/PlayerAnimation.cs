using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    public const string IS_RUNNING = "IsRunning";
    public const string IS_JUMPING = "IsJumping";
    
    private Animator animator;
    private Vector2 direction;
    private bool isJumping;
    private bool isPressJump;
    private float previousVelocityY = 0;
    private PlayerController playerController;
    private Rigidbody2D rb2d;
    private void Start() {
        rb2d = this.GetComponent<Rigidbody2D>();
        previousVelocityY = rb2d.linearVelocity.y;
        animator = this.GetComponent<Animator>();
        playerController = this.GetComponent<PlayerController>();
        if (gameObject.tag == "Player1")
        {
            GameInput.Instance.GetPlayerInputSystem().Player1.Jump.performed += ctx => JumpAnimation(ctx);
        }
        else
        {
            GameInput.Instance.GetPlayerInputSystem().Player2.Jump.performed += ctx => JumpAnimation(ctx);
        }
    }

    private void JumpAnimation(InputAction.CallbackContext ctx)
    {
        if (playerController.IsGrounded())
        {
            isJumping = true;
            isPressJump = true;
        }
        
    }

    public void Update() 
    {
        
        direction = playerController.GetDirectionVector();

        if (isPressJump)
        {
            StartCoroutine(PressJumpCounter());
        }
        if (playerController.IsGrounded() && !isPressJump)
        {
            isJumping = false;
        } 
        if (playerController.CanMove)
        {
            animator.SetBool(IS_RUNNING, direction.x != 0 && !isJumping);
            animator.SetBool(IS_JUMPING,isJumping);     
        }
    }

    IEnumerator PressJumpCounter()
    {
        yield return new WaitForSeconds(0.2f);
        isPressJump = false;
    }
}
