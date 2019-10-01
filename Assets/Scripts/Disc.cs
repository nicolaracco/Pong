using UnityEngine;

public class Disc : MonoBehaviour
{
    private float movementSpeed;
    private bool increaseSpeedOnBounce;

    private float currentMovementSpeed;

    private Rigidbody2D rb;

    private Vector2 movementDirection;

    public Vector2 MovementDirection { 
        get { return movementDirection; } 
        set {
            if (increaseSpeedOnBounce) {
                currentMovementSpeed *= 1.02f;
            }
            movementDirection = value;
            rb.velocity = movementDirection * currentMovementSpeed;
        }
    }

    public void OnMatchStateChanged(MatchStateTransition transition)
    {
        if (transition.newState == MatchState.GoalMade 
            || transition.newState == MatchState.Ended 
            || transition.newState == MatchState.Stopped
        ) {
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
            currentMovementSpeed = movementSpeed;
            rb.velocity = (currentMovementSpeed * 0.75f) * movementDirection;
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        movementSpeed = GameSettings.discMovementSpeed;
        increaseSpeedOnBounce = GameSettings.increaseSpeedOnBounce;
    }
}
