using Hungover.Interactables.Carryables;
using UnityEngine;

namespace Hungover.Interactables
{
    public class Door : Barricade
    {
        #region Editor Fields

        [SerializeField] private Key key = null;

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
