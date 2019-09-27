using UnityEngine;

namespace Pong.InputBehaviour
{
    public interface IInputBehaviour
    {
        float GetMovementInput(Vector2 currentPosition);
    }

    public class HumanInputBehaviour : IInputBehaviour
    {
        private string movementAxisName;

        public HumanInputBehaviour(string movementAxisName)
        {
            this.movementAxisName = movementAxisName;
        }

        public float GetMovementInput(Vector2 currentPosition)
        {
            return Input.GetAxisRaw(movementAxisName);
        }
    }

    public class AIInputBehaviour : IInputBehaviour
    {
        private Disc disc;

        private Vector2 discPosition
        {
            get { return disc.transform.localPosition; }
        }

        public AIInputBehaviour(Disc disc)
        {
            this.disc = disc;
        }

        public float GetMovementInput(Vector2 currentPosition)
        {
            float distanceToMove;
            if (!IsDiscMovingTowardsMe(currentPosition)) {
                distanceToMove = -currentPosition.y;
            } else {
                distanceToMove = discPosition.y - currentPosition.y;
            }
            return Mathf.Clamp(distanceToMove, -0.9f, 0.9f);
        }

        private bool IsDiscMovingTowardsMe(Vector2 position)
        {
            return Mathf.Sign(position.x - discPosition.x) == Mathf.Sign(disc.MovementDirection.x);
        }
    }
}