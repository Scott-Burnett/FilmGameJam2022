namespace Hungover.Interactables.Barricades
{
    public class BreakableBarrricade<BreakerType> : Barricade
    {
        #region Public Events

        public delegate void OnUnlockCallback(BreakableBarrricade<BreakerType> breakableBarricade);
        public OnUnlockCallback onUnlockCallback;

        #endregion

        #region Barricade Methods

        protected override bool ConditionsToUnlockAreMet(Interactor interactor) =>
            interactor.curentInteractable is BreakerType;

        protected override void OnUnlock(Interactor interactor)
        {
            // ToDo Play Particle Effect
            onUnlockCallback(this);
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