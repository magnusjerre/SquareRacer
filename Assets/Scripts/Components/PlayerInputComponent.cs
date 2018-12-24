using UnityEngine;
using Unity.Mathematics;

namespace Jerre
{
    public class PlayerInputComponent : MonoBehaviour
    {
        public Vector2 direction;
        public bool fire;
        public bool boost;
        public bool NoMovement => direction.sqrMagnitude <= Utils.ZERO_DELTA;

        public float2 getDirectionAsFloat2()
        {
            return new float2(direction.x, direction.y);
        }

        public Vector3 DirectionAsVector3() {
            return new Vector3(direction.x, 0, direction.y);
        }

    }
}
