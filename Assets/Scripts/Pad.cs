using UnityEngine;
using Pong.InputBehaviour;

public class Pad : MonoBehaviour
{
    public float movementSpeed = 10f;

    private Rigidbody2D rb;
    private AudioSource audioSource;

    private PlayerID playerID;
    private bool gameIsRunning = false;
    private IInputBehaviour inputBehaviour;

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
        if (collision.gameObject.GetComponent<Disc>() == null) {
            return;
        }
        audioSource.Play();
    }

    IInputBehaviour CreateInputBehaviour()
    {
        if (GameSettings.GetPlayerTypeForPlayerID(playerID) == PlayerType.AI) {
            return new AIInputBehaviour(GameObject.FindObjectOfType<Disc>());
        }
        return new HumanInputBehaviour(playerID == PlayerID.Left ? "2nd Vertical" : "Vertical");
    }
}
