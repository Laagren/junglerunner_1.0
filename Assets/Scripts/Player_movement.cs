using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;
    private Rigidbody body;
    public Transform pos;
    private Vector3 newPos;
    private Vector3 direction;
    public bool rightSideChosen, leftSideChosen;
    public Vector3 triggerPosition;
    private CapsuleCollider capsule;
    private BoxCollider col;
    public LayerMask floor;
    public GameObject childOfCurrentTile;
    public GameObject currentTile;
    public float gravity;
    public float distanceToFloor;
    private animationHandler animHandler;
    System.Random random = new System.Random();
    [SerializeField]
    private float sideSpeed;
    [SerializeField]
    private float runningSpeed;
    //private Vector3 boxOffset;
    private float idleTimer,  controllerSaveHeight, controllerSlideHeight, controllerSaveCenterY, controllerSlideCenterY;
    private bool startRunning, onFloor, canTurn, sliding, stopSideRun;
    public bool spawnTile;
    private Vector3 jumpForce;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
        capsule = GetComponent<CapsuleCollider>();
        body = GetComponent<Rigidbody>();
        //col = GetComponent<BoxCollider>();
        //boxOffset = new Vector3(0, 0.9f, 0);
        controllerSaveHeight = characterController.height;
        controllerSlideHeight = 0.7f;
        controllerSaveCenterY = characterController.center.y;
        controllerSlideCenterY = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (idleTimer < 3)
        {
            idleTimer += Time.deltaTime;
        }
        else
        {
            startRunning = true;
        }
        //if (animHandler.JumpIsFinished)
        //{
        //    animator.SetBool("")
        //}

        if (startRunning)
        {
            //newPos = pos.transform.position;
            onFloor = Physics.CheckSphere(transform.position, distanceToFloor, floor);
            animator.SetBool("running", true);
            //transform.Translate(Vector3.forward * Time.deltaTime * runningSpeed);
            

            if (Input.GetKeyDown(KeyCode.Space) && onFloor)
            {
                animator.SetBool("jumping", true);
                jumpForce.y = Mathf.Sqrt(1.5f * -2f * gravity);
                //body.isKinematic = false;
                //body.AddForce(Vector3.up * 4f, ForceMode.Impulse);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                animator.SetBool("sliding", true);
                sliding = true;
            }
            if (sliding)
            {
                characterController.height = controllerSlideHeight;
                characterController.center = new Vector3(0, controllerSlideCenterY, 0);
            }
            else
            {
                characterController.height = controllerSaveHeight;
                characterController.center = new Vector3(0, 0.85f, 0);
            }

            //if (Input.GetKey(KeyCode.A))
            //{
            //    sideSpeed = -5f;
            //}
            //else if (Input.GetKey(KeyCode.D))
            //{
            //    sideSpeed = 5f;
            //}

            //else
            //{
            //    sideSpeed = 0;
            //}
            if (canTurn)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    transform.Rotate(0, 90, 0);
                    //characterController.
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

            if (onFloor && jumpForce.y < 0)
            {
                //body.isKinematic = true;
                jumpForce.y = -2f;
            }

            jumpForce.y += gravity * Time.deltaTime;
            if (!stopSideRun)
            {
                direction = new Vector3(Input.GetAxis("Horizontal") * sideSpeed, 0, runningSpeed);

            }
            direction = transform.TransformDirection(direction);
            //direction *= runningSpeed;
            characterController.Move(direction * Time.deltaTime);
            //characterController.Move(new Vector3(sideSpeed, jumpForce.y, runningSpeed) * Time.deltaTime);
            characterController.Move(jumpForce * Time.deltaTime);
        }
        //col.center = transform.position + boxOffset;
    }

    
    private void OnTriggerEnter(Collider other)
    {
        // Kollar om spelaren sprungit in i en vägg (T-korsning) och svänger automatiskt.
        if (other.isTrigger && other.gameObject.tag == "WallBoth")
        {
            int i = random.Next(0, 2);
            if (i == 0) // Sväng höger
            {
                transform.Rotate(0, 90, 0);
            }
            else
            {
                transform.Rotate(0, -90, 0);
            }

            //saveRotation = transform.rotation;
            other.isTrigger = false;
        }
        if (other.isTrigger && other.gameObject.tag == "WallTurnRight")
        {
            transform.Rotate(0, 90, 0);
            //saveRotation = transform.rotation;
            other.isTrigger = false;
        }
        if (other.isTrigger && other.gameObject.tag == "WallTurnLeft")
        {
            transform.Rotate(0, -90, 0);
            //saveRotation = transform.rotation;
            other.isTrigger = false;
        }

        // Kollar om spelaren klivit på en trigger som gör att den kan vända 90 grader.
        if (other.isTrigger && other.gameObject.tag == "turnPlatform")
        {
            canTurn = true;
            //other.isTrigger = false;
        }
        if (other.isTrigger && other.gameObject.tag == "turnedLeftTrigger")
        {
            
            triggerPosition = new Vector3(other.transform.position.x +5, other.transform.position.y , other.transform.position.z + 41);
            Debug.Log("collsion");
            leftSideChosen = true;
        }
        if (other.isTrigger && other.gameObject.tag == "turnedRightTrigger")
        {
            triggerPosition = new Vector3(other.transform.position.x - 5, other.transform.position.y, other.transform.position.z + 41);
            Debug.Log("collsion");
            rightSideChosen = true;
        }
        if (other.gameObject.tag == "SpawnNewTile")
        {
            spawnTile = true;
        }
        if (other.gameObject.tag == "wall")
        {
            stopSideRun = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger && other.gameObject.tag == "turnPlatform")
        {
            canTurn = false;
        }
        if (other.gameObject.tag == "wall")
        {
            stopSideRun = false;
        }
        //if (other.isTrigger && other.gameObject.tag == "turnedLeftTrigger" || other.gameObject.tag == "turnedRightTrigger")
        //{
        //    spawnTwall = false;
        //}
        //if (other.isTrigger && other.gameObject.tag == "turnedLeftTrigger")
        //{
        //    Debug.Log("turnOffTrigger");
        //    spawnTwall = false;
        //}
        //if (other.isTrigger && other.gameObject.tag == "turnedRightTrigger")
        //{
        //    Debug.Log("TurnOffTrigger");
        //    spawnTwall = false;
        //}
    }

    public void SlideFinished(string message)
    {
        if (message.Equals("SlideFinished"))
        {
            animator.SetBool("sliding", false);
        }
    }

    public void JumpFinished(string message)
    {
        if (message.Equals("JumpFinished"))
        {
            animator.SetBool("jumping", false);
        }
    }

    public void SlideUp(string message)
    {
        if (message.Equals("SlideUp"))
        {
            sliding = false;
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

    }


}
