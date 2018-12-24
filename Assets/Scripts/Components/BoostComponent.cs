using UnityEngine;
using System.Collections;

namespace Jerre
{
    public class BoostComponent : MonoBehaviour
    {
        public float maxBoostTime = 1f;
        public float acceleration = 40f;
        public float breakFactor = 0.9f;
        public float breakDeceleration = 100f;
        public float speed = 20f;

        public float timeLeftOfBoost;
        public BoostState state;

        public float SpeedSqr => speed * speed;
    }
}
