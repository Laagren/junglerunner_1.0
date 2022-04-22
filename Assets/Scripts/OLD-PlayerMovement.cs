using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float runningSpeed = 20.0f;
    public float sideSpeed = 3.0f;
    public float jumpForce = 4.0f;
    private bool canJump = true;
    System.Random random = new System.Random();
    private Rigidbody body;
    private BoxCollider col;
    private Quaternion saveRotation;
    private Vector3 saveBoxSize, saveBoxCenter, boxJumpPos;
    private ParticleSystem particle;
    private bool started, canTurn, boxBackUp, goingDown;
    private bool running = true;
    Animator animator;
    private float score, runTimer, idleTimer, jumpTimer, boxJumpHeight, boxSaveHeight;
    private int lives = 3;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        col = GetComponent<BoxCollider>();
        saveBoxCenter = col.center;
        saveBoxSize = col.size;
        boxJumpHeight = 2;
        particle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        idleTimer += Time.deltaTime;
        
        if (idleTimer > 2f)
        {
            started = true;     
        }
        if (runTimer > 8f && runningSpeed <= 30f)
        {
            runningSpeed += 1f;
            runTimer = 0;
        }
        if (started)
        {
            runTimer += Time.deltaTime;
            // Rakt fram, v�nster och h�ger justering med A och D

            if (running)
            {
                animator.SetBool("IsRunning", true);
                transform.Translate(Vector3.forward * Time.deltaTime * runningSpeed);
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }



            //particle.
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * Time.deltaTime * sideSpeed);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * Time.deltaTime * sideSpeed);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                animator.SetBool("Slide", true);
                boxBackUp = false;
                // Om spelaren glider, �ndra storleken och positionen p� boxcollidern, liggande.
                col.size = new Vector3(1.06f, 0.64f, 1.9f);
                col.center = new Vector3(0.12f, 1f, 0.03f);
            }

            // Kollar om spelaren �r p� v�g att st�lla sig upp, �ndra boxcolliderns storlek till st�ende.
            if (boxBackUp)
            {
                col.size = saveBoxSize;
                col.center = saveBoxCenter;
            }

            // 90 graders sv�ng med pilar
            if (canTurn)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    transform.Rotate(0, 90, 0);
                    saveRotation = transform.rotation;
                    canTurn = false;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    transform.Rotate(0, -90, 0);
                    saveRotation = transform.rotation;
                    canTurn = false;
                }
            }
            // H�ller spelarens sparade rotation fr�n att �ndras.
            transform.rotation = saveRotation;

            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                canJump = false;
                animator.SetBool("hasJumped", true);
                //animator.SetBool("IsRunning", false);
                body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                boxJumpHeight = 4;
                boxJumpPos = new Vector3(col.center.x, boxJumpHeight, col.center.z);                            
            }
            if (canJump is false)
            {
                col.center = Vector3.Lerp(col.center, boxJumpPos, Time.deltaTime);
            }
            //if (goingDown)
            //{
            //    col.center = Vector3.Lerp(col.center, new Vector3(col.center.x, 0.8f, col.center.z), 0.05f);
            //}

        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            animator.SetBool("hasJumped", false);
            canJump = true;
            goingDown = false;
        }
    }

    // Metoder som kollar om animationerna har spelat klart.
    public void SlideFinished(string message)
    {
        if (message.Equals("SlideFinished"))
        {
            animator.SetBool("Slide", false);
        }
    }
    public void SlideBackUp(string message)
    {
        if (message.Equals("SlideBackUp"))
        {
            boxBackUp = true;
        }
    }

    public void JumpDown(string message)
    {
        if (message.Equals("JumpDown"))
        {
            col.center = new Vector3(col.center.x, 0.8f, col.center.z);
            goingDown = true;
            animator.SetBool("IsRunning", true);
        }
    }

    //public void JumpFinished(string message)
    //{
    //    if (message.Equals("JumpFinished"))
    //    {
    //        //hasJumped = false;

    //        animator.SetBool("hasJumped", false);
    //    }
    //}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // Kollar om spelaren sprungit in i en v�gg (T-korsning) och sv�nger automatiskt.
        if (other.isTrigger && other.gameObject.tag == "WallBoth")
        {
            int i = random.Next(0, 2);
            if (i == 0) // Sv�ng h�ger
            {
                transform.Rotate(0, 90, 0);
            }
            else
            {
                transform.Rotate(0, -90, 0);
            }
            
            saveRotation = transform.rotation;
            other.isTrigger = false;
        }
        if (other.isTrigger && other.gameObject.tag == "WallTurnRight")
        {
            transform.Rotate(0, 90, 0);
            saveRotation = transform.rotation;
            other.isTrigger = false;
        }
        if (other.isTrigger && other.gameObject.tag == "WallTurnLeft")
        {
            transform.Rotate(0, -90, 0);
            saveRotation = transform.rotation;
            other.isTrigger = false;
        }

        // Kollar om spelaren klivit p� en trigger som g�r att den kan v�nda 90 grader.
        if (other.isTrigger && other.gameObject.tag == "turnPlatform")
        {
            canTurn = true;
        }

        if (other.isTrigger && other.gameObject.tag == "roadBlock")
        {
            running = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger && other.gameObject.tag == "turnPlatform")
        {
            canTurn = false;
        }
    }
}
