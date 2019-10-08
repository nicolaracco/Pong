using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong.PadBehaviours
{
    public class AIPad : AbstractPad
    {
        Disc disc;
        IEnumerator trackPathCoroutine;
        Vector2? trackedDiscDestinationPoint;

        public override void OnMatchStateChanged(MatchStateTransition transition)
        {
            base.OnMatchStateChanged(transition);
            if (transition.oldState == MatchState.Running) {
                StopTrackPathCoroutine();
            } else if (transition.newState == MatchState.Running) {
                StopTrackPathCoroutine();
                StartCoroutine(TrackPath());
            }
        }

        protected override void Awake()
        {
            base.Awake();
            disc = GameObject.FindObjectOfType<Disc>();
        }

        protected override float GetMovementInput()
        {
            if (!IsDiscMovingTowardsMe() || trackedDiscDestinationPoint == null) {
                return 0f;
            }
            Vector2 endPosition = trackedDiscDestinationPoint.Value;
            float destinationYPosition = findYCoordinateOnStraightLine(disc.Position, endPosition, transform.position.x);
            return Mathf.Clamp(destinationYPosition - transform.position.y, -1f, 1f);
        }

        bool IsDiscMovingTowardsMe()
        {
            return Mathf.Sign(transform.position.x) == Mathf.Sign(disc.MovementDirection.x);
        }

        void StopTrackPathCoroutine()
        {
            if (trackPathCoroutine == null) {
                return;
            }
            StopCoroutine(trackPathCoroutine);
            trackPathCoroutine = null;
        }

        IEnumerator TrackPath()
        {
            while (gameIsRunning) {
                trackedDiscDestinationPoint = GetDiscDestinationPoint();
                yield return new WaitForSeconds(.25f);
            }
        }

        Vector2? GetDiscDestinationPoint()
        {
            int netLayer = LayerMask.NameToLayer("Nets");
            int collisionMasks = LayerMask.GetMask("Walls", "Nets");
            Vector2 prevPosition = disc.Position;
            Vector2 prevMovementDirection = disc.MovementDirection;
            LinkedList<RaycastHit2D> foundHits = new LinkedList<RaycastHit2D>();
            RaycastHit2D hit = Physics2D.CircleCast(prevPosition + prevMovementDirection, disc.Radius, prevMovementDirection, Mathf.Infinity, collisionMasks);
            while (hit.collider != null) {
                foundHits.AddLast(hit);
                if (foundHits.Count == 2) {
                    Debug.DrawLine(prevPosition, hit.point, Color.red, .25f);
                    return null;
                } else if (hit.collider.gameObject.layer == netLayer) {
                    Debug.DrawLine(prevPosition, hit.point, Color.green, .25f);
                    return hit.point;
                }
                Debug.DrawLine(prevPosition, hit.point, Color.yellow, .25f);
                prevMovementDirection = new Vector2(prevMovementDirection.x, - prevMovementDirection.y);
                hit = Physics2D.CircleCast(prevPosition + prevMovementDirection, disc.Radius, prevMovementDirection, Mathf.Infinity, collisionMasks);
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