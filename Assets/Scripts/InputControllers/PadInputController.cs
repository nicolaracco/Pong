using UnityEngine;

namespace Pong.InputControllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Pad))]
    public class PadInputController : MonoBehaviour
    {
        public float destinationInputSensitivity = 100f;
        public float deltaInputSensitivity = 250f;

        IPadControls padControls;
        Rigidbody2D rb;

        public void OnMatchStateChanged(MatchStateTransition transition)
        {
            enabled = transition.newState == MatchState.Running;
            if (transition.newState == MatchState.GoalMade || transition.newState == MatchState.Ended) {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }
        }

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            Pad pad = GetComponent<Pad>();
            padControls = CreatePadControls(pad.playerID, pad.playerType);
            if (pad.playerType == PlayerType.AI) {
                destinationInputSensitivity *= 0.35f;
                deltaInputSensitivity *= 0.35f;
            }
        }

        void FixedUpdate()
        {
            if (!padControls.CurrentInputValue.HasValue)
                return;
            InputValue inputValue = padControls.CurrentInputValue.Value;
            if (inputValue.type == InputValueType.Destination) {
                float delta = inputValue.value - transform.position.y;
                rb.AddForce(new Vector2(0, delta * destinationInputSensitivity));
            } else {
                rb.AddForce(new Vector2(0, inputValue.value * deltaInputSensitivity));
            }
        }

        IPadControls CreatePadControls(PlayerID playerID, PlayerType playerType)
        {
            if (playerType == PlayerType.AI)
                return gameObject.AddComponent<AIPlayerInputControls>();
            else if (playerID == PlayerID.Left)
                return gameObject.AddComponent<LeftHumanPlayerInputControls>();
            return gameObject.AddComponent<RightHumanPlayerInputControls>();
        }
    }
}