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

        public float GetMovementInput(Vector2 currentPosition, float padSpeed)
        {
            if (!IsDiscMovingTowardsMe(currentPosition)) {
                return 0f;
            }
            Vector2? endPosition = trackDiscEndOfPath();
            if (endPosition == null) {
                return 0f;
            }
            float destinationYPosition = findYCoordinateOnStraightLine(disc.Position, endPosition.Value, currentPosition.x);
            return Mathf.Clamp(destinationYPosition - currentPosition.y, -1f, 1f);
        }

        private bool IsDiscMovingTowardsMe(Vector2 position)
        {
            return Mathf.Sign(position.x) == Mathf.Sign(disc.MovementDirection.x);
        }

        private Vector2? trackDiscEndOfPath()
        {
            int netLayer = LayerMask.NameToLayer("Nets");
            int collisionMasks = LayerMask.GetMask("Walls", "Nets");
            Vector2 prevPosition = disc.Position;
            Vector2 prevMovementDirection = disc.MovementDirection;
            RaycastHit2D hit = Physics2D.CircleCast(prevPosition, disc.Radius, prevMovementDirection, Mathf.Infinity, collisionMasks);
            while (hit.collider != null) {
                if (hit.collider.gameObject.layer == netLayer) {
                    return hit.point;
                }
                // this works only if walls are on top and bottom
                Vector2 radiusDelta = new Vector2(0, Mathf.Sign(prevMovementDirection.y) * disc.Radius);
                prevPosition = hit.point - radiusDelta;
                // this works only if walls are on top and bottom
                prevMovementDirection = new Vector2(prevMovementDirection.x, - prevMovementDirection.y);
                hit = Physics2D.CircleCast(prevPosition, disc.Radius, prevMovementDirection, Mathf.Infinity, collisionMasks);
            }
            return null;
        }

        /*
         * Using the formula (X - X1) / (X2 - X1) = (Y - Y1) / (Y2 - Y1)
         * Thanks to Roberta, my mathematician sister
         */
        private float findYCoordinateOnStraightLine(Vector2 start, Vector2 end, float x)
        {
            return start.y + ((x - start.x) * (end.y - start.y)) / (end.x - start.x);
        }
    }
}