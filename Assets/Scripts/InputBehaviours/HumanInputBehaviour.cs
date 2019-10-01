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
            if (SystemInfo.deviceType == DeviceType.Desktop) {
                return GetDesktopMovementInput(currentPosition);
            }
            return GetHandheldMovementInput(currentPosition);
        }

        float GetHandheldMovementInput(Vector2 currentPosition)
        {
            Vector2? touchPosition = GetHandheldPressedTouchPosition();
            if (touchPosition.HasValue) {
                return Mathf.Clamp(touchPosition.Value.y - currentPosition.y, -1, 1);
            }
            return 0f;
        }

        Vector2? GetHandheldPressedTouchPosition()
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
        
        public float GetDesktopMovementInput(Vector2 currentPosition)
        {
            return Input.GetAxisRaw(GetDesktopMovementAxisName());
        }

        string GetDesktopMovementAxisName()
        {
            if (Screen.width > Screen.height) {
                return playerId == PlayerID.Left ? "2nd Vertical" : "Vertical";
            }
            return playerId == PlayerID.Left ? "2nd Horizontal" : "Horizontal";
        }
   }
}