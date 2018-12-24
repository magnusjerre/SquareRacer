using UnityEngine;
using System.Collections;

namespace Jerre
{
    public class Utils
    {
        public const float ZERO_DELTA = 0.000001f;

        public static Vector3 Min(Vector3 a, Vector3 b) {
            if (a.sqrMagnitude < b.sqrMagnitude) {
                return a;
            }
            return b;
        }

        public static Vector3 Max(Vector3 a, Vector3 b) {
            if (a.sqrMagnitude > b.sqrMagnitude) {
                return a;
            }
            return b;
        }
    }
}
