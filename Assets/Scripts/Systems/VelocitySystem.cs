using UnityEngine;
using System.Collections;
using Unity.Entities;

namespace Jerre
{
    public class VelocitySystem : ComponentSystem
    {
        struct Data {
            public CharacterController controller;
            public PlayerInputComponent input;
            public BoostComponent boost;
            public VelocityComponent velocity;
            public VehicleSettingsComponent vehicle;
        }

        protected override void OnUpdate()
        {
            var dt = Time.deltaTime;

            foreach (var entity in GetEntities<Data>()) {
                var controller = entity.controller;
                var input = entity.input;
                var boost = entity.boost;
                var velocity = entity.velocity;
                var vehicle = entity.vehicle;
                var speed = boost.state == BoostState.BOOSTING ? boost.speed : vehicle.maxSpeed;

                var inputSpeed = input.DirectionAsVector3() * speed;
                var angleBetweenOldSpeedAndInputSpeed = Vector3.Angle(velocity.velocity.normalized, input.DirectionAsVector3());
                var inertia = angleBetweenOldSpeedAndInputSpeed >= 90 || System.Math.Abs(input.direction.sqrMagnitude) < 0.0001f ? vehicle.breakInertia : vehicle.inertia;
                var diff = (inputSpeed - velocity.velocity) * inertia;
                velocity.velocity = velocity.velocity + diff;

                controller.Move(velocity.velocity * dt);
            }
        }
    }
}
