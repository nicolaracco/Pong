using UnityEngine;

public class Disc : MonoBehaviour
{
    public float movementSpeedOnStart = 2.5f;
    public float movementSpeed = 5f;

    private Rigidbody2D rb;

    private Vector2 movementDirection;

    public Vector2 MovementDirection { 
        get { return movementDirection; } 
        set {
            movementDirection = value;
            rb.velocity = movementDirection * movementSpeed;
        }
    }

    public void OnMatchStateChanged(MatchStateTransition transition)
    {
        if (transition.newState == MatchState.GoalMade || transition.newState == MatchState.Ended || transition.newState == MatchState.Stopped) {
            transform.localPosition = Vector3.zero;
            rb.velocity = Vector2.zero;
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
}
