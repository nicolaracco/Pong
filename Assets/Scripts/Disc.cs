using UnityEngine;

public class Disc : MonoBehaviour
{
    public float startMovementSpeed = 15f;
    public bool increaseSpeedOnBounce = true;
    public float currentMovementSpeed; // public only for being able to see it in inspector

    Rigidbody2D rb;
    GameSettings gameSettings;

    Vector2 movementDirection;

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
            currentMovementSpeed = startMovementSpeed;
            rb.velocity = (currentMovementSpeed * 0.75f) * movementDirection;
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameSettings = GameSettings.Current;
    }

    void Start()
    {
        if (gameSettings != null) {
            increaseSpeedOnBounce = !gameSettings.classicMode;
        }
    }
}
