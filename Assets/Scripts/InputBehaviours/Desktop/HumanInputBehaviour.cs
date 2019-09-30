#if !UNITY_ANDROID && !UNITY_IPHONE
using UnityEngine;

namespace Pong.InputBehaviour
{
    public class HumanInputBehaviour : InputBehaviourInterface
    {
        PlayerID playerId;

        public HumanInputBehaviour(PlayerID playerId)
        {
            this.playerId = playerId;
        }

        public float GetMovementInput(Vector2 currentPosition)
        {
            return Input.GetAxisRaw(GetMovementAxisName());
        }

        string GetMovementAxisName()
        {
            return playerId == PlayerID.Left ? "2nd Vertical" : "Vertical";
        }
    }
}
#endif