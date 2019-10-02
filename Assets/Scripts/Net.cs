using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    public GoalEvent OnGoalEvent;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        audioSource.enabled = GameSettings.audioEnabled;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Disc>() == null) {
            return;
        }
        audioSource.Play();
        OnGoalEvent.Invoke(transform.localPosition.x > 0 ? PlayerID.Left : PlayerID.Right);
    }
}
