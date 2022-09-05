using FMODUnity;
using UnityEngine;

namespace Hungover.Interactables
{
    public abstract class Barricade : Interactable
    {
        #region Editor Fields

        [Header("Sounds")]
        [SerializeField] private StudioEventEmitter openEmitter = null;
        [SerializeField] private StudioEventEmitter closeEmitter = null;
        [SerializeField] private StudioEventEmitter lockedEventEmitter = null;
        [SerializeField] private StudioEventEmitter unlockEventEmitter = null;

        #endregion

        #region Public Fields

        [HideInInspector] public bool isLocked = true;
        [HideInInspector] public bool isOpen = false;

        #endregion

        #region Abstract Methods

        protected abstract bool ConditionsToUnlockAreMet(Interactor interactor);

        #endregion

        #region Virtual Methods

        protected virtual void OnUnlock(Interactor interactor){}
        protected virtual void Open(){}
        protected virtual void Close(){}

        #endregion

        #region Interactable Methods

        protected override void Initialise()
        {
            SetLayerRecursively(Constants.doorLayer);
        }

        public override bool CausesDisposeCurrentInteractable() => false;

        public override void OnInteract(Interactor interactor)
        {
            if (isLocked &&
                ConditionsToUnlockAreMet(interactor))
            {
                isLocked = false;
                unlockEventEmitter?.Play();
                OnUnlock(interactor);
            }

            if (isLocked)
            {
                lockedEventEmitter?.Play();
                return;
            }

            if (isOpen)
            {
                Close();
                closeEmitter?.Play();
                isOpen = false;
            }
            else
            {
                Open();
                openEmitter?.Play();
                isOpen = true;
            }
        }

        public override Sprite Indicator() => 
            isLocked ? MainUI.Instance.lockCrosshairSprite : MainUI.Instance.unlockCrosshairSprite;

        #endregion
    }
}