using UnityEngine;

namespace Jerre
{
    public class WeaponComponent : MonoBehaviour
    {
        public float minTimeBetweenFires;
        public float speed = 30f;
        public float timeSinceLastFire;

        public bool CanFire => timeSinceLastFire >= minTimeBetweenFires;
    }
}
