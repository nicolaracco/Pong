using UnityEngine;

namespace Pong.PadBehaviours
{
    public abstract class AbstractPad : MonoBehaviour
    {
        public float movementSpeed = 30f;
        public PlayerID playerId;
        public bool audioEnabled = false;

        Rigidbody2D rb;
        GameSettings gameSettings;
        Collider2D selfCollider;
        AudioSource audioSource;

        protected bool gameIsRunning = false;

        public virtual void OnMatchStateChanged(MatchStateTransition transition)
        {
            gameIsRunning = transition.newState == MatchState.Running;
            if (transition.newState == MatchState.GoalMade || transition.newState == MatchState.Ended) {
                transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
            }
        }

        protected abstract float GetMovementInput();

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            gameSettings = GameSettings.Current;
            selfCollider = GetComponent<Collider2D>();
            audioSource = GetComponent<AudioSource>();
        }

        protected void Start()
        {
            if (gameSettings != null) {
                audioEnabled = gameSettings.audioEnabled;
            }
        }

        protected void FixedUpdate()
        {
            rb.velocity = gameIsRunning 
                ? new Vector2(0, movementSpeed * GetMovementInput()) 
                : Vector2.zero;
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            Disc disc = collision.gameObject.GetComponent<Disc>();
            if (disc == null) {
                return;
            }
            // collider height as it appears in unity inspect
            Vector2 colliderSize = selfCollider.bounds.size;
            // collision point in local coordinates
            Vector2 collisionPoint = transform.InverseTransformPoint(collision.contacts[0].point);
            // hit factor, ranges in -0.5f,+0.5f
            float hitFactor = collisionPoint.y / colliderSize.y;
            // (0.25f * PI = 45 deg) * (2 * hitFactor = [-1,1]) => resulting angle ranges in -45/45
            float phi = 0.5f * Mathf.PI * hitFactor;
            disc.MovementDirection = Quaternion.AngleAxis(Mathf.Rad2Deg * phi, Vector3.forward) * transform.right;
            if (audioEnabled) {
                audioSource.Play();
            }
        }
    }
}