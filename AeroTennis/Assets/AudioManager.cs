using UnityEngine;
using Mirror;
using System.Collections.Generic;

public class AudioManager : NetworkBehaviour
{
    [SerializeField] private AudioClip[] soundClips;
    private Dictionary<string, AudioClip> soundDictionary;

    private void Start()
    {
        // Create a dictionary to map sound names to their corresponding AudioClip
        soundDictionary = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in soundClips)
        {
            soundDictionary.Add(clip.name, clip);
        }
    }

    [Command]
    private void CmdPlaySound(string soundName )
    {
        RpcPlaySound(soundName);
    }

    [ClientRpc]
    private void RpcPlaySound(string soundName)
    {
        
        float volume = 1f;
        if (!isLocalPlayer)
        {
            volume = .2f;
        }
        
        AudioSource audioSource = GetComponent<AudioSource>();
     
        if (audioSource != null && soundDictionary.ContainsKey(soundName))
        {
            audioSource.PlayOneShot(soundDictionary[soundName], volume);
        }
        
        
    }

    // Call this method from other scripts to play a sound by name
    public void PlaySound(string soundName)
    {
        if (isLocalPlayer)
        {
            CmdPlaySound(soundName);
        }
    }
}