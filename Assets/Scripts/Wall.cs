using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        audioSource.enabled = GameSettings.audioEnabled;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Disc>() == null) {
            return;
        }
        if (audioSource.isActiveAndEnabled) {
            audioSource.Play();
        }
    }
}
