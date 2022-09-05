
using FMODUnity;
using UnityEngine;

namespace Hungover
{
    public abstract class Interactable : MonoBehaviour
    {
        #region Private Members

        private StudioEventEmitter interactAudioEmitter;

        #endregion

        #region Abstract Methods
        
        public abstract void OnInteract(Interactor interactor);
        public abstract Sprite Indicator();

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
            TryGetComponent<StudioEventEmitter>(out interactAudioEmitter);

            SetLayerRecursively(Constants.interactableLayer);
            Initialise();
        }
        
        #endregion

        #region Public Methods

        public void ShowInteractableIndicator()
        {
            MainUI.Instance.ShowInteractableIndicator(Indicator());
        }

        public void HideInteractableIndicator()
        {
            MainUI.Instance.ShowDefaultCrosshair();
        }

        public void PlayInteractableSound()
        {
            interactAudioEmitter?.Play();
        }

        #endregion

        #region Protected Methods

        protected void DisableInteractSound()
        {
            interactAudioEmitter = null;
        }

        #endregion
    }
}