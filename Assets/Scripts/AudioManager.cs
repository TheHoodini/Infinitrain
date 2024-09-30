using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource audioSource;

    // Sound Effects
    public AudioClip scoreSound;
    public AudioClip gameOverSound;
    public AudioClip eatSound;
    public AudioClip powerUpSound;

    private void Awake()
    {
        instance = this;
        audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.pitch = 1.8f;
        audioSource.volume = 0.4f;
    }
    public void ScoreSound()
    {
        audioSource.PlayOneShot(scoreSound);
    }

    public void GameOverSound()
    {
        audioSource.pitch = 1.5f;
        audioSource.PlayOneShot(gameOverSound);
    }

    public void EatSound()
    {
        audioSource.PlayOneShot(eatSound);
    }

    public void PowerUpSound()
    {
        audioSource.PlayOneShot(powerUpSound);
    }
}
