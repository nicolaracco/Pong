using UnityEngine;

namespace Pong.InputControllers
{
    public abstract class HumanPlayerInputControls : MonoBehaviour, IPadControls
    {
        public InputValue? CurrentInputValue { get; private set; }

        protected void Update()
        {
            CurrentInputValue = FetchInputValue();
        }

        private InputValue? FetchInputValue()
        {
            Vector2? touchPosition = GetPressedTouchPosition();
            Vector2? mousePosition = GetMouseDragPosition();
            float axisValue = Input.GetAxisRaw(GetMovementAxisName());
            if (touchPosition.HasValue) {
                return new InputValue(touchPosition.Value.y, InputValueType.Destination);
            } else if (mousePosition.HasValue) {
                return new InputValue(mousePosition.Value.y, InputValueType.Destination);
            } else if (axisValue != 0) {
                return new InputValue(axisValue, InputValueType.Delta);
            }
            return null;
        }

        protected abstract bool IsPositionInPlayerArea(Vector2 position);

        Vector2? GetMouseDragPosition()
        {
            if (Input.GetMouseButton(0)) {
                Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (IsPositionInPlayerArea(position))
                    return position;
            }
            return null;
        }

        Vector2? GetPressedTouchPosition()
        {
            foreach (Touch t in Input.touches) {
                Vector2 position = Camera.main.ScreenToWorldPoint(t.position);
                if (IsPositionInPlayerArea(position))
                    return position;
            }
            return null;
        }

        protected abstract string GetMovementAxisName();
    }
}