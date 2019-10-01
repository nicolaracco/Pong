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
            Vector2? touchPosition = GetPressedTouchPosition();
            if (touchPosition.HasValue) {
                return Mathf.Clamp(touchPosition.Value.y - currentPosition.y, -1, 1);
            }
            return Mathf.Clamp(Input.GetAxisRaw(GetMovementAxisName()), -0.75f, 0.75f);
        }

        Vector2? GetPressedTouchPosition()
        {
            foreach (Touch t in Input.touches) {
                Vector2 position = Camera.main.ScreenToWorldPoint(t.position);
                if ((playerId == PlayerID.Left && position.x < 0) ||
                    (playerId == PlayerID.Right && position.x > 0)) {
                        return position;
                    }
            }
            return null;
        }

        string GetMovementAxisName()
        {
            if (Screen.width > Screen.height) {
                return playerId == PlayerID.Left ? "2nd Vertical" : "Vertical";
            }
            return playerId == PlayerID.Left ? "2nd Horizontal" : "Horizontal";
        }
   }
}