using UnityEngine;
using System.Collections;
using Unity.Entities;

namespace Jerre
{
    public class BoostSystem : ComponentSystem
    {
        struct Data
        {
            public PlayerInputComponent playerInput;
            public BoostComponent boost;
        }

        protected override void OnUpdate()
        {
            var dt = Time.deltaTime;

            foreach (var entity in GetEntities<Data>()) {
                var input = entity.playerInput;
                var boost = entity.boost;

                switch (boost.state) {
                    case BoostState.BOOSTING: {
                            if (input.boost)
                            {
                                if (boost.timeLeftOfBoost > 0f)
                                {
                                    boost.state = BoostState.BOOSTING;
                                    boost.timeLeftOfBoost = Mathf.Max(0f, boost.timeLeftOfBoost - dt);
                                }
                                else
                                {
                                    boost.state = BoostState.CHARGING;
                                    boost.timeLeftOfBoost = Mathf.Min(boost.maxBoostTime, boost.timeLeftOfBoost + dt);
                                }
                            } else {
                                boost.state = BoostState.CHARGING;
                                boost.timeLeftOfBoost = Mathf.Min(boost.maxBoostTime, boost.timeLeftOfBoost + dt);
                            }
                            break;    
                        }
                    case BoostState.CHARGING: {
                            boost.timeLeftOfBoost += dt;
                            if (boost.timeLeftOfBoost >= boost.maxBoostTime) {
                                boost.timeLeftOfBoost = boost.maxBoostTime;
                                boost.state = BoostState.NOTHING;
                            }
                            break;
                        }
                    case BoostState.NOTHING: {
                            if (input.boost) {
                                boost.state = BoostState.BOOSTING;
                                boost.timeLeftOfBoost -= dt;
                            }
                            break;
                        }
                    default: {
                            break;
                        }
                }
            }
        }

    }
}
