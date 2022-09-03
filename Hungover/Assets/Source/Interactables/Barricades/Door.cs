using UnityEngine;

namespace Hungover.Interactables
{
    public class Door : Barricade
    {
        #region Editor Fields

        [SerializeField] private Interactable key = null;

        #endregion

        #region Abstract Methods

        protected override bool ConditionsToUnlockAreMet(Interactor interactor) =>
            interactor.curentInteractable == key;

        #endregion

        #region Interactable Methods

        public override void OnUpdate(){}
        public override void OnDispose(){}

        #endregion
    }
}
