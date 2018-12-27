using UnityEngine;

namespace Jerre
{
    public class Bullet : MonoBehaviour
    {
        public float explosionRadius = 1f;
        public float timeUntilInputsWorkAgain = 2f;
        public float speed = 40f;
        public float timeToLive = 3f;

        private void Start()
        {
            Destroy(gameObject, timeToLive);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Bullet.OnTriggerEnter");
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (var coll in colliders)
            {
                var player = coll.GetComponent<PlayerSettingsComponent>();
                if (player != null && player.receiveInput)
                {
                    var explosion = Instantiate(BootStrap.bootStrapInstance.explosionSpawnPrefab);
                    explosion.playerNumber = player.playerNumber;
                    explosion.timeLeftOfExplosionEffect = timeUntilInputsWorkAgain;

                }
            }
            Destroy(gameObject);
        }
    }
}
