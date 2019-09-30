using UnityEngine;

namespace Pong.InputBehaviour
{
    public interface InputBehaviourInterface
    {
        float GetMovementInput(Vector2 currentPosition);
    }
}