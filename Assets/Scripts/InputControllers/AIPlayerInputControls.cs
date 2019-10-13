using UnityEngine;
using Pong.AI;

namespace Pong.InputControllers
{
    [RequireComponent(typeof(PathTracker))]
    [RequireComponent(typeof(Collider2D))]
    public class AIPlayerInputControls : MonoBehaviour, IPadControls
    {
        public InputValue? CurrentInputValue { get; private set; }

        Collider2D selfCollider;
        Disc disc;
        PathTracker pathTracker;
        float? lastNeededYToInterceptDisc;

        void Awake()
        {
            pathTracker = GetComponent<PathTracker>();
            selfCollider = GetComponent<Collider2D>();
            disc = GameObject.FindObjectOfType<Disc>();
        }

        void OnEnable()
        {
            pathTracker.enabled = true;
        }

        void OnDisable()
        {
            pathTracker.enabled = false;
        }

        void Update()
        {
            if (pathTracker.neededYToInterceptDisc.HasValue) {
                if (pathTracker.neededYToInterceptDisc != lastNeededYToInterceptDisc) {
                    lastNeededYToInterceptDisc = pathTracker.neededYToInterceptDisc;
                    CurrentInputValue = new InputValue(
                        CalculateNeededTargetY(pathTracker.neededYToInterceptDisc.Value),
                        InputValueType.Destination
                    );
                }
            } else if (transform.position.y != 0) {
                CurrentInputValue = new InputValue(Mathf.Clamp(0 - transform.position.y, -1f, 1f), InputValueType.Delta);
            }
        }

        /*
         * Given the disc start position and end position, this returns the y position where
         * the pad should place itself to be able to get the disc
         */
        float CalculateNeededTargetY(float neededPadY)
        {
            float boundYMin = selfCollider.bounds.min.y;
            float boundYMax = selfCollider.bounds.max.y;
            float nearestYBound = neededPadY < transform.position.y ? boundYMin + disc.Radius : boundYMax - disc.Radius;
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