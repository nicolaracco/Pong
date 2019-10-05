using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    public bool audioEnabled;
    public GoalEvent OnGoalEvent;

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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Disc>() == null) {
            return;
        }
        if (audioEnabled) {
            audioSource.Play();
        }
        OnGoalEvent.Invoke(transform.localPosition.x > 0 ? PlayerID.Left : PlayerID.Right);
    }
}
