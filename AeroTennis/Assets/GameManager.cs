using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject objectToDisable;
    public GameObject objectToEnable;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public float delayBeforeAction = 32f;

    private bool hasPerformedAction = false;

    void Start()
    {
        Invoke("PerformAction", delayBeforeAction);
    }

    void PerformAction()
    {
        if (!hasPerformedAction)
        {
            if (objectToDisable != null)
                objectToDisable.SetActive(false);

            if (objectToEnable != null)
                objectToEnable.SetActive(true);

            if (audioSource != null && audioClip != null)
                audioSource.PlayOneShot(audioClip);

            hasPerformedAction = true;
        }
    }
}