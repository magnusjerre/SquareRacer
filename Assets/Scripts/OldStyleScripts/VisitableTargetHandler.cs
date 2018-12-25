using UnityEngine;

namespace Jerre
{
    public class VisitableTargetHandler : MonoBehaviour, IObserver
    {
        public int point = 1;

        private VisitableTarget[] allTargets;
        private int indexOfCurrentTarget = -1;
        private GameManager gameManager;

        private void Start()
        {
            allTargets = GameObject.FindObjectsOfType<VisitableTarget>();
            foreach (var target in allTargets) {
                target.AddListener(this);
                target.SetActiveTarget(false);
            }

            Random.InitState(0);
            ChooseNextTarget();

            gameManager = GameObject.FindObjectOfType<GameManager>();
        }

        private void ChooseNextTarget() {
            var nextNumber = Random.Range(0, allTargets.Length - 1);
            if (nextNumber == indexOfCurrentTarget) {
                nextNumber = (nextNumber + 1) % allTargets.Length;
            }

            if (indexOfCurrentTarget != -1) {
                allTargets[indexOfCurrentTarget].SetActiveTarget(false);
            }

            allTargets[nextNumber].SetActiveTarget(true);

            this.indexOfCurrentTarget = nextNumber;
        }

        public void MJNotify(object obj1)
        {
            if (obj1 is TargetVisited) {
                var targetVisited = (TargetVisited) obj1;
                gameManager.RegisterPointsForPlayer(targetVisited.player, point);
                ChooseNextTarget();
            }
        }
    }
}
