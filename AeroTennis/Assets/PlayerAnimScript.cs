using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine;

using UnityEngine;
using System.Collections;

public class PlayerAnimScript : NetworkBehaviour
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
        if (!isLocalPlayer)
            return;

        if (Input.GetKey(KeyCode.D))
        {
            movingRight = true;
            movingLeft = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movingLeft = true;
            movingRight = false;
        }
        else
        {
            movingLeft = false;
            movingRight = false;
        }

        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        animator.SetBool("movingRight", movingRight);
        animator.SetBool("movingLeft", movingLeft);
    }
    
    public void PlayAnimation(string animation)
    {
        animator.Play(animation);
        StartCoroutine(ReturnToDefaultState(animation));
    }

    
    IEnumerator ReturnToDefaultState(string animationName)
    {
        AnimationClip animationClip = GetAnimationClip(animationName);
        if (animationClip != null)
        {
            yield return new WaitForSeconds(animationClip.length);
            animator.Play("idleAnim");
        }
    }

    private AnimationClip GetAnimationClip(string clipName)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == clipName)
            {
                return clip;
            }
        }
        Debug.LogWarning("Animation clip " + clipName + " not found!");
        return null;
    }
}
