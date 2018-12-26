using UnityEngine;

namespace Jerre
{
    public class VehicleSettingsComponent : MonoBehaviour
    {
        public float maxSpeed = 10f;
        public float acceleration = 20f;
        public float deceleration = 10f;
        public float radius = 1f;

        public float SpeedSquared => maxSpeed * maxSpeed;
    }
}
