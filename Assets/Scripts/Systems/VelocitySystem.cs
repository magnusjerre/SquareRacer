using UnityEngine;
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
                AccelerationBasedVelocityHandler(entity, dt);
            }
        }

        private void AccelerationBasedVelocityHandler(Data entity, float dt) {
            var controller = entity.controller;
            var input = entity.input;
            var boost = entity.boost;
            var vel = entity.velocity;
            var vehicle = entity.vehicle;

            var oldVelocity = vel.velocity;
            var speed = boost.state == BoostState.BOOSTING ? boost.speed : vehicle.maxSpeed;
            var acceleration = boost.state == BoostState.BOOSTING ? boost.acceleration * dt: vehicle.acceleration * dt;
            var deceleration = input.NoMovement ? (-oldVelocity.normalized) * vehicle.deceleration : Vector3.zero;

            var inputVelocity = input.DirectionAsVector3() * acceleration;
            var newVelocity = oldVelocity + deceleration * dt + inputVelocity;
            var maxSpeedSqr = boost.state == BoostState.BOOSTING ? boost.SpeedSqr : vehicle.SpeedSquared;
            if (newVelocity.sqrMagnitude > maxSpeedSqr) {
                var newVelocityDirection = newVelocity.normalized;
                var decel = newVelocityDirection * boost.breakDeceleration * dt;
                newVelocity = Utils.Max(newVelocityDirection * speed, newVelocity - decel);
            }

            vel.velocity = newVelocity;
            controller.Move(vel.velocity * dt);
        }
    }
}
