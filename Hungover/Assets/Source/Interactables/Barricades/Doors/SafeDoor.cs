namespace Hungover.Interactables.Doors
{
    public class SafeDoor : Door
    {
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

        #endregion
    }
}