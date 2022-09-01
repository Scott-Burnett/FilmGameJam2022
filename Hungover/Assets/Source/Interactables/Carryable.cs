namespace Hungover.Interactables
{
    public abstract class Carryable : Interactable
    {
        #region Interactable Methods

        public override void OnInteract(Interactor interactor)
        {
            interactor.state = Interactor.State.Carrying;
        }

        #endregion
    }
}