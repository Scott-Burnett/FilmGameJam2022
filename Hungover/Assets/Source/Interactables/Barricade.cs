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

        #region protected Fields

        protected bool isLocked = true;
        protected bool isOpen = false;

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

        public override void OnInteract(Interactor interactor)
        {
            if (ConditionsToUnlockAreMet(interactor) &&
                isLocked)
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

        #endregion
    }
}