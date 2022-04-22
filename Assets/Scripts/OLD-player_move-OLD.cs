using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_move : MonoBehaviour
{
    public float runSpeed;
    private CharacterController character;
    private Animator animator;
    public LayerMask floor;
    private Vector3 jumpForce;
    public float distanceToFloor;
    public float sideSpeed;
    private bool onFloor, running, jumping, sliding, idle, canTurn;
    private float gravity, idleTimer, runningFloat, jumpFloat, slideFloat;
    void Start()
    {
        character = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        running = false;
        jumping = false;
        sliding = false;
        idle = true;
        gravity = -9.81f;
        runningFloat = 0f;
        jumpFloat = 0.5f;
        slideFloat = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!idle)
        {
            if (running)
            {
                animator.SetFloat("Blend", runningFloat);
            }
            if (jumping)
            {
                animator.SetFloat("Blend", jumpFloat);
            }
            if (sliding)
            {
                animator.SetFloat("Blend", slideFloat);
            }

            if (Input.GetKeyDown(KeyCode.Space) && onFloor)
            {
                jumpForce.y = Mathf.Sqrt(1.5f * -2f * gravity);
                jumping = true;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                sliding = true;
            }
            if (canTurn)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    transform.Rotate(0, 90, 0);
                    //saveRotation = transform.rotation;
                    canTurn = false;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    transform.Rotate(0, -90, 0);
                    //saveRotation = transform.rotation;
                    canTurn = false;
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                sideSpeed = 5f;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                sideSpeed = 5f;
            }
            else
            {
                sideSpeed = 0;
            }

            onFloor = Physics.CheckSphere(transform.position, distanceToFloor, floor);
            if (onFloor && jumpForce.y < 0)
            {
                jumpForce.y = -2f;
            }

            character.Move(new Vector3(sideSpeed, 0, runSpeed) * Time.deltaTime);

            jumpForce.y += gravity * Time.deltaTime;
            character.Move(jumpForce * Time.deltaTime);


        }   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger && other.gameObject.tag == "turnPlatform")
        {
            canTurn = true;
        }
    }

    public void SlideFinished(string message)
    {
        if (message.Equals("SlideFinished") || message.Equals("JumpFinished"))
        {
            animator.SetFloat("Blend", runningFloat);
        }
    }

    public void IdleFinished(string message)
    {
        if (message.Equals("IdleFinished"))
        {
            animator.SetBool("started", true);
            idle = false;
        }
    }
}
