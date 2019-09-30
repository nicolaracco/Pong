using UnityEngine;

namespace Pong.InputBehaviour
{
    public class AIInputBehaviour : InputBehaviourInterface
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