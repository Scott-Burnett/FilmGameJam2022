using UnityEngine;

namespace Hungover.Interactables
{
    public class BarricadeTest : Barricade
    {
        #region Abstract Methods

        protected override bool ConditionsToUnlockAreMet(Interactor interactor)
        { 
            return false;
        }

        #endregion

        #region Interactable Methods

        public override void OnInteract(Interactor interactor)
        {
            base.OnInteract(interactor);
        }

        public override void OnUpdate(){}
        public override void OnDispose(){}

        #endregion
    }
}