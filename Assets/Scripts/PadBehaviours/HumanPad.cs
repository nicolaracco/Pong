using UnityEngine;

namespace Pong.PadBehaviours
{
    public class HumanPad : AbstractPad
    {
        protected override float GetMovementInput()
        {
            Vector2? touchPosition = GetPressedTouchPosition();
            if (touchPosition.HasValue) {
                return Mathf.Clamp(touchPosition.Value.y - transform.position.y, -1, 1);
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