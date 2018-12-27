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
            if (GameInfo.gameState != GameState.PLAYING) {
                return;
            }

            foreach (var entity in GetEntities<Data>()) {
                var input = entity.input;
                var settings = entity.settings;

                if (settings.receiveInput) {
                    var number = settings.playerNumber;
                    var x = Input.GetAxis(InputNames.HORIZONTAL + number);
                    var y = Input.GetAxis(InputNames.VERTICAL + number);

                    input.direction = new Vector2(x, y).normalized;
                    input.fire = Input.GetButtonDown(InputNames.FIRE + number);
                    input.boost = Input.GetButton(InputNames.BOOST + number) || Input.GetButtonDown(InputNames.BOOST + number);
                } else {
                    input.direction = Vector2.zero;
                    input.fire = false;
                    input.boost = false;
                }
            }
        }
    }
}
