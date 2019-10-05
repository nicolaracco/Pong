using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public bool audioEnabled = false;

    AudioSource audioSource;
    GameSettings gameSettings;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        gameSettings = GameSettings.Current;
    }

    void Start()
    {
        if (gameSettings != null) {
            audioEnabled = gameSettings.audioEnabled;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Disc>() == null) {
            return;
        }
        if (audioEnabled) {
            audioSource.Play();
        }
    }
}
