using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pong.AI;

namespace Pong.PadBehaviours
{
    public class AIPad : AbstractPad
    {
        public float pathRecalcTime = .2f;
        public int maxPathNodes = 2;

        Disc disc;
        IEnumerator trackPathCoroutine;
        TrackedPath? lastTrackedPath;
        float? targetYPosition;

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
            if (!IsDiscMovingTowardsMe() || !targetYPosition.HasValue) {
                return 0f;
            }
            return Mathf.Clamp(targetYPosition.Value - transform.position.y, -1f, 1f);
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
            PathTracker pathTracker = new PathTracker();
            while (gameIsRunning) {
                if (IsDiscMovingTowardsMe()) {
                    TrackedPath? path = pathTracker.TrackPath(disc, maxPathNodes);
                    if (path.HasValue) {
                        path.Value.Debug(pathRecalcTime);
                        // if path end position is not similar to the previous tracked one
                        if (!path.Value.IsSimilar(lastTrackedPath)) {
                            lastTrackedPath = path;
                            targetYPosition = CalculateNeededTargetY(
                                path.Value.LastBouncePosition, 
                                path.Value.ApproximatedEndPosition
                            );
                        }
                    } else {
                        // cannot determine target position
                        targetYPosition = null;
                    }
                }
                yield return new WaitForSeconds(pathRecalcTime);
            }
        }

        /*
         * Given the disc start position and end position, this returns the y position where
         * the pad should place itself to be able to get the disc
         */
        float CalculateNeededTargetY(Vector2 start, Vector2 end)
        {
            float neededPadY = FindYCoordinateOnStraightLine(start, end, transform.position.x);
            Debug.DrawLine(transform.position, new Vector2(transform.position.x, neededPadY), Color.gray, pathRecalcTime);
            float nearestYBound = neededPadY < transform.position.y ? boundYMin + disc.Radius : boundYMax - disc.Radius;
            float? oldTargetYPosition = targetYPosition;
            return transform.position.y + neededPadY - nearestYBound;
        }

        /*
         * Using the formula (X - X1) / (X2 - X1) = (Y - Y1) / (Y2 - Y1)
         * Thanks to Roberta, my mathematician sister
         */
        float FindYCoordinateOnStraightLine(Vector2 start, Vector2 end, float x)
        {
            return start.y + ((x - start.x) * (end.y - start.y)) / (end.x - start.x);
        }
    }
}