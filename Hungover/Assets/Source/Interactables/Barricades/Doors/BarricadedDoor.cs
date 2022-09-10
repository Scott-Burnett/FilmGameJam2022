using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hungover.Interactables.Barricades.Doors
{
    public class BarricadedDoor : Door
    {
        #region Editor Fields

        [SerializeField] private List<Barricade> barricades = new List<Barricade>();

        #endregion

        #region Door Methods

        protected override bool ConditionsToUnlockAreMet(Interactor interactor) => 
            barricades.All(barricade => !barricade.isLocked) &&
            base.ConditionsToUnlockAreMet(interactor);

        protected override void Initialise()
        {
            base.Initialise();
            isLocked = barricades.Count > 0;
        }

        #endregion
    }
}