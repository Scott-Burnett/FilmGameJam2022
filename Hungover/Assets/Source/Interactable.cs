
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

        #endregion

        #region Virtual Methods

        protected virtual void Initialise(){}
        public virtual void OnUpdate(){}
        public virtual void OnDispose(){}

        protected void SetLayerRecursively(int layerIndex)
        {
            gameObject.layer = layerIndex;
            foreach (Transform item in transform)
            {
                item.gameObject.layer = layerIndex;
            } 
        }

        public virtual bool CausesDisposeCurrentInteractable() => true;

        #endregion

        #region Monobehaviour Methods

        private void Start()
        {
            TryGetComponent<StudioEventEmitter>(out audioEmitter);

            SetLayerRecursively(Constants.interactableLayer);
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