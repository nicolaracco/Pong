using UnityEngine;

namespace Pong.InputControllers
{
    public class LeftHumanPlayerInputControls : HumanPlayerInputControls
    {
        protected override bool IsPositionInPlayerArea(Vector2 position)
        {
            return position.x < 0;
        }

        protected override string GetMovementAxisName()
        {
            return Screen.width > Screen.height ? "2nd Vertical" : "2nd Horizontal";
        }
    }
}