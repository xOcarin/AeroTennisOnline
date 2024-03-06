using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimScript : MonoBehaviour
{
    private Animator animator;
    private bool movingRight = false;
    private bool movingLeft = false;

    void Start()
    {
        // Get the Animator component attached to the object
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            movingRight = true;
            movingLeft = false;
            animator.SetBool("movingRight", movingRight);
            animator.SetBool("movingLeft", movingLeft);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            movingLeft = true;
            movingRight = false;
            animator.SetBool("movingRight", movingRight);
            animator.SetBool("movingLeft", movingLeft);
        }
        else
        {
            movingLeft = false;
            movingRight = false;
            animator.SetBool("movingRight", movingRight);
            animator.SetBool("movingLeft", movingLeft);
        }
    }
}
