using System.Collections;
using UnityEngine;

namespace Pong.AI
{
    public class PathTracker : MonoBehaviour
    {
        public int maxTraversingDepth = 2;
        public float pathRecalcTime = .2f;

        public float? neededYToInterceptDisc;

        Disc disc;
        Coroutine trackPathCoroutine;
        TrackedPath? lastTrackedPath;

        void Awake()
        {
            disc = GameObject.FindObjectOfType<Disc>();
        }

        void Start()
        {
            trackPathCoroutine = StartCoroutine(TrackPath());
        }

        void OnDisable()
        {
            if (trackPathCoroutine == null)
                return;
            StopCoroutine(trackPathCoroutine);
            trackPathCoroutine = null;
        }

        IEnumerator TrackPath()
        {
            TrackPath pathTracker = new TrackPath();
            while (true) {
                if (IsDiscMovingTowardsMe()) {
                    TrackedPath? path = pathTracker.Execute(disc, maxTraversingDepth);
                    if (path.HasValue) {
                        path.Value.Debug(pathRecalcTime);
                        // if path end position is not similar to the previous tracked one
                        if (!path.Value.IsSimilar(lastTrackedPath)) {
                            lastTrackedPath = path;
                            neededYToInterceptDisc = path.Value.InterceptOnXAxis(transform.position.x);
                        }
                    } else
                        neededYToInterceptDisc = null; // cannot determine target position
                } else
                    neededYToInterceptDisc = null; // disc not moving toward disc
                yield return new WaitForSeconds(pathRecalcTime);
            }
        }

        bool IsDiscMovingTowardsMe()
        {
            return Mathf.Sign(transform.position.x) == Mathf.Sign(disc.MovementDirection.x);
        }
    }
}