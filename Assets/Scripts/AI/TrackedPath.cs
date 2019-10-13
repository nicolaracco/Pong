using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Pong.AI
{
    public struct TrackedPath
    {
        public struct Node
        {
            public static Node BuildFromRaycastHit2D(Vector2 position, Vector2 direction, RaycastHit2D hit)
            {
                return new Node(position, direction, hit.centroid, hit.point);
            }

            public readonly Vector2 startPosition;
            public readonly Vector2 endPosition;
            public readonly Vector2 hitPosition;
            public readonly Vector2 direction;

            public Node(Vector2 startPosition, Vector2 direction, Vector2 endPosition, Vector2 hitPosition)
            {
                this.startPosition = startPosition;
                this.direction = direction;
                this.endPosition = endPosition;
                this.hitPosition = hitPosition;
            }

            public Vector2 MidPoint
            { 
                get { return startPosition + (endPosition - startPosition) * 0.5f; } 
            }

            public void Debug(Color color, float duration)
            {
                UnityEngine.Debug.DrawLine(startPosition, endPosition, color, duration);
            }
        }

        public readonly IEnumerable<TrackedPath.Node> nodes;
        public readonly bool trackedToNet;

        public TrackedPath(IEnumerable<TrackedPath.Node> nodes, bool trackedToNet)
        {
            Assert.IsTrue(nodes.Count() > 0, "A tracked path must contain at least one node");
            this.nodes = nodes;
            this.trackedToNet = trackedToNet;
        }

        public Node LastNode { 
            get { return nodes.Last(); }
        }

        public Vector2 LastBouncePosition
        { 
            get { return LastNode.startPosition; } 
        }

        public Vector2 ApproximatedEndPosition
        { 
            get { return trackedToNet ? LastNode.hitPosition : LastNode.MidPoint; } 
        }

        public void Debug(float duration)
        {
            foreach ((Node node, int index) in nodes.Select((node, index) => (node, index))) {
                if (index == nodes.Count() - 1) {
                    node.Debug(trackedToNet ? Color.green : Color.red, duration);
                } else {
                    node.Debug(Color.yellow, duration);
                }
            }
        }

        public bool IsSimilar(TrackedPath? other)
        {
            if (other == null) {
                return false;
            }
            Vector2 endPosition = this.ApproximatedEndPosition;
            Vector2 otherEndPosition = other.Value.ApproximatedEndPosition;
            return Mathf.Abs(Vector2.Distance(endPosition, otherEndPosition)) < 0.2f;
        }

        /*
         * Returns the y axis value needed to intercept the disc
         */
        public float InterceptOnXAxis(float x)
        {
            return FindYCoordinateOnStraightLine(LastBouncePosition, ApproximatedEndPosition, x);
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