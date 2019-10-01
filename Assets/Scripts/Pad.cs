using UnityEngine;
using Pong.InputBehaviour;

public class Pad : MonoBehaviour
{
    private float movementSpeed = 30f;

    private Rigidbody2D rb;
    private Collider2D selfCollider;
    private AudioSource audioSource;

    private PlayerID playerID;
    private bool gameIsRunning = false;
    private InputBehaviourInterface inputBehaviour;

    public void OnMatchStateChanged(MatchStateTransition transition)
    {
        gameIsRunning = transition.newState == MatchState.Running;
        if (transition.newState == MatchState.GoalMade || transition.newState == MatchState.Ended) {
            transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        selfCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        playerID = gameObject.name.StartsWith("Left") ? PlayerID.Left : PlayerID.Right;
    }

    void Start()
    {
        inputBehaviour = CreateInputBehaviour();
    }

    void FixedUpdate()
    {
        rb.velocity = gameIsRunning 
            ? new Vector2(0, movementSpeed * inputBehaviour.GetMovementInput(transform.localPosition)) 
            : Vector2.zero;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Disc disc = collision.gameObject.GetComponent<Disc>();
        if (disc == null) {
            return;
        }
        Vector3 colliderSize = selfCollider.bounds.size;
        float hitFactor = (disc.transform.localPosition.y - transform.localPosition.y) / colliderSize.y;
        disc.MovementDirection = new Vector2(- disc.MovementDirection.x, hitFactor * 2).normalized;
        audioSource.Play();
    }

    InputBehaviourInterface CreateInputBehaviour()
    {
        if (GameSettings.GetPlayerTypeForPlayerID(playerID) == PlayerType.AI) {
            return new AIInputBehaviour(GameObject.FindObjectOfType<Disc>());
        }
        return new HumanInputBehaviour(playerID);
    }
}
