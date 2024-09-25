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

    private void Awake()
    {
        instance = this;
        audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.pitch = 1.8f;
        audioSource.volume = 0.4f;
    }
    void Start()
    {

    }

    void Update()
    {

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
}
