using Hungover;
using UnityEngine;

namespace Hungover
{
    public abstract class Interactable : MonoBehaviour
    {
        #region Abstract Methods

        public abstract void OnInteract(Interactor interactor);
        public abstract void OnUpdate();
        public abstract void OnDispose();

        #endregion

        #region Monobehaviour Methods

        private void Start()
        {
            gameObject.layer = Constants.interactableLayer;
        }

        private void Update()
        {

        }
        
        #endregion
    }
}