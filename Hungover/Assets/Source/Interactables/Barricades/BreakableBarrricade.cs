namespace Hungover.Interactables.Barricades
{
    public class BreakableBarrricade<BreakerType> : Barricade
    {
        #region Public Fields

        public delegate void OnUnlockCallback(BreakableBarrricade<BreakerType> breakableBarricade);
        public OnUnlockCallback onUnlockCallback;
        public bool isBroken = false;

        #endregion

        #region Barricade Methods

        protected override bool ConditionsToUnlockAreMet(Interactor interactor) =>
            interactor.curentInteractable is BreakerType;

        public override bool CausesDisposeCurrentInteractable() => false;

        protected override void OnUnlock(Interactor interactor)
        {
            // ToDo Play Particle Effect
            onUnlockCallback?.Invoke(this);
            isBroken = true;
            Destroy(this.gameObject);
        }

        public override void OnUpdate()
        {

        }

        public override void OnDispose()
        {

        }

        #endregion
    }
}