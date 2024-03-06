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
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            movingRight = true;
        }
        else if(Input.GetAxisRaw("Horizontal") < 0)
        {
            movingLeft = true;
        }
        else
        {
            movingLeft = false;
            movingRight = false;
        }
        animator.SetBool("movingRight", movingRight);
        animator.SetBool("movingLeft", movingLeft);
    }
}
