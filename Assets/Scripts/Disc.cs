using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc : MonoBehaviour
{
    public float movementSpeedOnStart = 2.5f;
    public float movementSpeed = 5f;
    public GoalEvent OnGoal;    

    private Rigidbody2D rb;

    private Vector2 movementDirection;

    public Vector2 MovementDirection { get { return movementDirection; } }

    public void OnMatchStateChanged(MatchStateTransition transition)
    {
        if (transition.newState == MatchState.GoalMade || transition.newState == MatchState.Ended || transition.newState == MatchState.Stopped) {
            transform.localPosition = Vector3.zero;
        } else if (transition.newState == MatchState.Running) {
            if (transition.lastGoalPlayerID.HasValue) {
                movementDirection = new Vector2(
                    transition.lastGoalPlayerID.Value == PlayerID.Left ? 1 : -1,
                    Random.Range(-1f, 1f)
                ).normalized;
            } else {
                movementDirection = new Vector2(
                    Mathf.Sign(Random.Range(-1, 1)), 
                    Random.Range(-1f, 1f)
                ).normalized;
            }
            rb.velocity = movementSpeedOnStart * movementDirection;
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Net>() == null) {
            return;
        }
        rb.velocity = Vector2.zero;
        OnGoal.Invoke(transform.localPosition.x > 0 ? PlayerID.Left : PlayerID.Right);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 colliderSize = collision.collider.bounds.size;
        Vector3 collidedTransformPos = collision.transform.localPosition;
        if (collision.gameObject.GetComponent<Pad>() != null) {
            float hitFactor = (transform.localPosition.y - collidedTransformPos.y) / colliderSize.y;
            movementDirection = new Vector2(- movementDirection.x, hitFactor).normalized;
            rb.velocity = movementSpeed * movementDirection;
        }
    }
}
