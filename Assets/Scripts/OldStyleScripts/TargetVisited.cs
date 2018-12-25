using System;

namespace Jerre
{
    public struct TargetVisited
    {
        public PlayerSettingsComponent player;
        public VisitableTarget target;

        public TargetVisited(PlayerSettingsComponent player, VisitableTarget target) {
            this.player = player;
            this.target = target;
        }
    }
}
