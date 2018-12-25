using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Jerre
{
    public class VisitableTarget : MonoBehaviour
    {
        private bool activeTarget;
        public bool ActiveTarget { get { return activeTarget;  } }

        private Observable observable = new Observable();

        public void Start() {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (!activeTarget) return;
            
            var player = other.GetComponent<PlayerSettingsComponent>();
            if (player != null) {
                observable.NotifyListeners(new TargetVisited(player, this));
            }
        }

        public void AddListener(IObserver listener)
        {
            observable.AddObserver(listener);
        }

        public void RemoveListener(IObserver listener)
        {
            observable.AddObserver(listener);
        }

        public void SetActiveTarget(bool val) {
            GetComponentInChildren<MeshRenderer>().enabled = val;
            activeTarget = val;
        }
    }
}
