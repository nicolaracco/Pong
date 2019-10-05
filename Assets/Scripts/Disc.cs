using UnityEngine;

public class Disc : MonoBehaviour
{
    public float startMovementSpeed = 15f;
    public bool increaseSpeedOnBounce = true;
    public float currentMovementSpeed; // public only for being able to see it in inspector

    Rigidbody2D rb;
    GameSettings gameSettings;

    public Vector2 MovementDirection { 
        get { return rb.velocity.normalized; } 
        set {
            if (increaseSpeedOnBounce) {
                currentMovementSpeed *= 1.02f;
            }
            rb.velocity = value * currentMovementSpeed;
        }
    }

    public void OnMatchStateChanged(MatchStateTransition transition)
    {
        if (IsPauseState(transition.newState)) {
            transform.localPosition = Vector3.zero;
            rb.velocity = Vector2.zero;
        } else if (transition.newState == MatchState.Running) {
            Vector2 movementDirection = transition.lastGoalPlayerID.HasValue
                ? new Vector2(transition.lastGoalPlayerID.Value == PlayerID.Left ? 1 : -1, Random.Range(-1f, 1f))
                : new Vector2(Mathf.Sign(Random.Range(-1, 1)), Random.Range(-1f, 1f));
            currentMovementSpeed = startMovementSpeed;
            rb.velocity = (currentMovementSpeed * 0.75f) * movementDirection.normalized;
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

    bool IsPauseState(MatchState state)
    {
        return state == MatchState.GoalMade || state == MatchState.Ended || state == MatchState.Stopped;
    }
}
