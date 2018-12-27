using UnityEngine;
using System.Collections;
using Unity.Entities;

namespace Jerre
{
    public class BulletVelocitySystem : ComponentSystem
    {
        struct Data {
            public Bullet bullet;
            public Transform bulletTransform;
        }

        protected override void OnUpdate()
        {
            var dt = Time.deltaTime;

            foreach (var entity in GetEntities<Data>()) {
                var bullet = entity.bullet;
                var bt = entity.bulletTransform;

                bt.Translate(Vector3.forward * bullet.speed * dt);
            }
        }
    }
}
