using UnityEngine;
using Unity.Entities;

namespace Jerre
{
    public class WeaponSystem : ComponentSystem
    {
        struct Data {
            public readonly int Length;
            public readonly ComponentArray<PlayerInputComponent> inputs;
            public readonly ComponentArray<WeaponComponent> weapons;
        }

        [Inject] Data data;

        protected override void OnUpdate()
        {
            var dt = Time.deltaTime;

            for (var i = 0; i < data.Length; i++) {
                var input = data.inputs[i];
                var weapon = data.weapons[i];

                weapon.timeSinceLastFire += dt;
                if (input.fire && weapon.CanFire) {
                    var pt = input.transform;
                    var bullet = Object.Instantiate(BootStrap.bootStrapInstance.empBullet, pt.position + pt.forward * 2, pt.rotation);
                }
            }
        }
    }
}
