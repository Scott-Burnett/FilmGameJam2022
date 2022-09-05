namespace Hungover.Interactables.Carryables
{
    public class Crowbar : Carryable
    {
        #region Interactable Methods

        protected override void Initialise()
        {
            base.Initialise();
            DisableInteractSound();
        }

        #endregion
    }
}