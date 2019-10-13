using UnityEngine;

namespace Pong.InputControllers
{
    public class RightHumanPlayerInputControls : HumanPlayerInputControls
    {
        protected override bool IsPositionInPlayerArea(Vector2 position)
        {
            return position.x > 0;
        }

        protected override string GetMovementAxisName()
        {
            return Screen.width > Screen.height ? "Vertical" : "Horizontal";
        }
    }
}