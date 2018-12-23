using UnityEngine;
using System.Collections;

namespace Jerre
{
    public class BoostComponent : MonoBehaviour
    {
        public float maxBoostTime = 1f;
        public float speed = 20f;

        public float timeLeftOfBoost;
        public BoostState state;
    }
}
