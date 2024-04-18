using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager2 : MonoBehaviour
{
    // Reference to the AudioSource component
    private AudioSource audioSource;

    // Array to hold the sound clips
    [SerializeField] private AudioClip[] soundClips;

    // Dictionary to map sound names to their corresponding AudioClip
    private Dictionary<string, AudioClip> soundDictionary;

    private void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();

        // Initialize the dictionary
        soundDictionary = new Dictionary<string, AudioClip>();

        // Populate the dictionary with sound clips
        foreach (AudioClip clip in soundClips)
        {
            soundDictionary.Add(clip.name, clip);
        }
    }

    // Play a sound by name
    public void PlaySound(string soundName, float volume)
    {
        // Check if the dictionary contains the specified sound name
        if (soundDictionary.ContainsKey(soundName))
        {
            // Play the corresponding AudioClip
            audioSource.PlayOneShot(soundDictionary[soundName], volume);
        }
        else
        {
            // Log a warning if the sound name is not found in the dictionary
            Debug.LogWarning("Audio clip " + soundName + " not found.");
        }
    }
}