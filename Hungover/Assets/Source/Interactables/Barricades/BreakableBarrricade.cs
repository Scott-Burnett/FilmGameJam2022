namespace Hungover.Interactables.Barricades
{
    public class BreakableBarrricade<BreakerType> : Barricade
    {
        #region Barricade Methods

        protected override bool ConditionsToUnlockAreMet(Interactor interactor) =>
            interactor.currentInteractable is BreakerType;

        protected override void OnUnlock(Interactor interactor)
        {
            Destroy(this.gameObject);
        }

        #endregion
    }
}