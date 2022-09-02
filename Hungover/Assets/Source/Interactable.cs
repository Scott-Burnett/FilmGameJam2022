
using FMODUnity;
using UnityEngine;

namespace Hungover
{
    public abstract class Interactable : MonoBehaviour
    {
        #region Private Members

        private StudioEventEmitter audioEmitter;

        #endregion

        #region Abstract Methods
        
        public abstract void OnInteract(Interactor interactor);
        public abstract void OnUpdate();
        public abstract void OnDispose();

        #endregion

        #region Virtual Methods

        protected virtual void Initialise(){}

        #endregion

        #region Monobehaviour Methods

        private void Start()
        {
            TryGetComponent<StudioEventEmitter>(out audioEmitter);

            gameObject.layer = Constants.interactableLayer;
            Initialise();
        }
        
        #endregion

        #region Public Methods

        public void ShowInteractableIndicator()
        {

        }

        public void HideInteractableIndicator()
        {
            
        }

        public void PlayInteractableSound()
        {
            audioEmitter?.Play();
        }

        #endregion
    }
}