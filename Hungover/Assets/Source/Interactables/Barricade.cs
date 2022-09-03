namespace Hungover.Interactables
{
    public abstract class Barricade : Interactable
    {
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
            if (ConditionsToUnlockAreMet(interactor))
            {
                isLocked = false;
                OnUnlock(interactor);
            }

            if (isLocked)
            {
                return;
            }

            if (isOpen)
            {
                Close();
                isOpen = false;
            }
            else
            {
                Open();
                isOpen = true;
            }
        }

        #endregion
    }
}