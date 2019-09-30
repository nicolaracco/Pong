#if UNITY_ANDROID || UNITY_IPHONE

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
            Touch? touch = GetPressedTouch();
            if (touch != null) {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.Value.position);
                return Mathf.Clamp(touchPosition.y - currentPosition.y, -1, 1);
            }
            return 0f;
        }

        Touch? GetPressedTouch()
        {
            int screenCenter = Screen.width / 2;
            foreach (Touch t in Input.touches) {
                if (
                    (playerId == PlayerID.Left && t.position.x < screenCenter) ||
                    (playerId == PlayerID.Right && t.position.x > screenCenter)
                ) {
                    return t;
                }
            }
            return null;
        }
   }
}
#endif