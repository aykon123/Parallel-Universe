using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public AudioClip[] musicTracks;
    private AudioSource audioSource;
    private int currentIndex = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Set the initial track
        PlayNextTrack();
    }

    private void Update()
    {
        // Check if the current track has finished playing
        if (!audioSource.isPlaying)
        {
            // Play the next track
            PlayNextTrack();
        }
    }

    private void PlayNextTrack()
    {
        // Increment the index to get the next track
        currentIndex++;
        if (currentIndex >= musicTracks.Length)
        {
            currentIndex = 0;
        }

        // Set the new track and play it
        audioSource.clip = musicTracks[currentIndex];
        audioSource.Play();
    }
}
