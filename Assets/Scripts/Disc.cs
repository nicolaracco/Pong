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
        if (collider.gameObject.name == "LeftNet") {
            rb.velocity = Vector2.zero;
            OnGoal.Invoke(PlayerID.Right);
        } else if (collider.gameObject.name == "RightNet") {
            rb.velocity = Vector2.zero;
            OnGoal.Invoke(PlayerID.Left);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 colliderSize = collision.collider.bounds.size;
        Vector3 collidedTransformPos = collision.transform.position;
        if (collision.gameObject.name == "LeftPad") {
            float hitFactor = (transform.position.y - collidedTransformPos.y) / colliderSize.y;
            movementDirection = new Vector2(1, hitFactor).normalized;
            rb.velocity = movementSpeed * movementDirection;
        } else if (collision.gameObject.name == "RightPad") {
            float hitFactor = (transform.position.y - collidedTransformPos.y) / colliderSize.y;
            movementDirection = new Vector2(-1, hitFactor).normalized;
            rb.velocity = movementSpeed * movementDirection;
        }
    }
}
