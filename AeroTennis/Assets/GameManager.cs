using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject objectToEnable;
    public GameObject objectToDisable;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public float delayBeforeAction = 32f;

    private bool hasPerformedAction = false;

    void Start()
    {
        Invoke("PerformAction", delayBeforeAction);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PerformAction();
        }
    }

    void PerformAction()
    {
        if (!hasPerformedAction)
        {
            if (videoPlayer != null)
            {
                videoPlayer.Stop();
                objectToDisable.SetActive(false);
            }

            if (objectToEnable != null)
                objectToEnable.SetActive(true);

            if (audioSource != null && audioClip != null)
                audioSource.PlayOneShot(audioClip);

            hasPerformedAction = true;
        }
    }
}
