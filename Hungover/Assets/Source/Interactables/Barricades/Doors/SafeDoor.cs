using UnityEngine;

namespace Hungover.Interactables.Doors
{
    public class SafeDoor : Door
    {
        #region Editor Fields

        [Space(10), Header("Safe References")]
        [SerializeField] private SafeInteractable safe = null;

        #endregion

        #region Public Methods

        public void Unlock()
        {
            codeHasBeenEntred = true;
        }

        #endregion

        #region Private Fields

        private bool codeHasBeenEntred = false;

        #endregion

        #region Door Methods

        protected override bool ConditionsToUnlockAreMet(Interactor interactor) =>
            codeHasBeenEntred;

        protected override void Initialise()
        {
            base.Initialise();
            isLocked = true;
        }

        public override void OnInteract(Interactor interactor)
        {
            if (!codeHasBeenEntred)
            {
                interactor.currentInteractable?.OnDispose();
                interactor.EndInteraction();
                safe.OnInteract(interactor);
                interactor.currentInteractable = safe;
            }
            else
            {
                base.OnInteract(interactor);
            }
        }

        #endregion
    }
}