using UnityEngine;
using System.Collections;
using Unity.Entities;

namespace Jerre
{
    public class PlayerInputSystem : ComponentSystem
    {
        struct Data
        {
            public PlayerInputComponent input;
            public PlayerSettingsComponent settings;
        }

        protected override void OnUpdate()
        {
            foreach (var entity in GetEntities<Data>()) {
                var input = entity.input;
                var settings = entity.settings;

                var number = settings.playerNumber;
                var x = Input.GetAxis(InputNames.HORIZONTAL + number);
                var y = Input.GetAxis(InputNames.VERTICAL + number);

                input.direction = new Vector2(x, y).normalized;
                input.fire = Input.GetButtonDown(InputNames.FIRE + number);
                input.boost = Input.GetButton(InputNames.BOOST + number) || Input.GetButtonDown(InputNames.BOOST + number);
                input.join = Input.GetButtonDown(InputNames.JOIN + number);
                input.leave = Input.GetButtonDown(InputNames.LEAVE + number);
            }
        }
    }
}
