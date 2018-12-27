using UnityEngine;

namespace Jerre
{
    public class BootStrapMono : MonoBehaviour
    {
        public ExplosionEmpComponent explosionSpawnPrefab;
        public Bullet empBullet;

        private void Awake()
        {
            BootStrap.bootStrapInstance = this;
        }
    }
}
