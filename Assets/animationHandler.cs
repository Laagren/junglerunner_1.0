using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationHandler : MonoBehaviour
{
    private bool slideFinished, jumpFinished, sliding;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
       animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void SlideStart(string message)
    //{
    //    if (message.Equals("SlideStart"))
    //    {
    //        //slideFinished = false;
    //        animator.SetBool("sliding", true);
    //    }
    //}
    public void SlideFinished(string message)
    {
        if (message.Equals("SlideFinished"))
        {
            //slideFinished = true;'
            animator.SetBool("sliding", false);
        }
    }

    //public void JumpStart(string message)
    //{
    //    if (message.Equals("JumpStart"))
    //    {
    //        //jumpFinished = false;
    //        animator.SetBool("jumping", true);
    //    }
    //}
    public void JumpFinished(string message)
    {
        if (message.Equals("JumpFinished"))
        {
            //jumpFinished = true;
            animator.SetBool("jumping", false);
        }
    }
}
