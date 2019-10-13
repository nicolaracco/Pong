using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pong.AI
{
    public class TrackPath
    {
        int netLayer;
        int collisionMasks;
        
        public TrackPath()
        {
            netLayer = LayerMask.NameToLayer("Nets");
            collisionMasks = LayerMask.GetMask("Walls", "Nets");
        }

        public TrackedPath? Execute(Disc disc, int maxNodes = 2)
        {
            Vector2 prevPosition = disc.Position;
            Vector2 prevMovementDirection = disc.MovementDirection;
            LinkedList<TrackedPath.Node> nodes = new LinkedList<TrackedPath.Node>();
            RaycastHit2D hit = Physics2D.CircleCast(prevPosition + prevMovementDirection, disc.Radius, prevMovementDirection, Mathf.Infinity, collisionMasks);
            while (hit.collider != null) {
                nodes.AddLast(TrackedPath.Node.BuildFromRaycastHit2D(prevPosition, prevMovementDirection, hit));
                if (hit.collider.gameObject.layer == netLayer) {
                    return new TrackedPath(nodes, true);
                } else if (nodes.Count > maxNodes) {
                    return new TrackedPath(nodes, false);
                } 
                prevPosition = hit.centroid;
                prevMovementDirection = Vector2.Reflect(prevMovementDirection, hit.normal);
                hit = Physics2D.CircleCast(prevPosition + prevMovementDirection, disc.Radius, prevMovementDirection, Mathf.Infinity, collisionMasks);
            }
            return null;
        }
    }
}