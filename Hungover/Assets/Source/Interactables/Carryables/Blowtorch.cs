using FMODUnity;
using Hungover.Interactables.Barricades.BreakableBarricades;
using UnityEngine;

namespace Hungover.Interactables.Carryables
{
    public class Blowtorch : Carryable
    {
        #region Editor Fields

        [SerializeField] private StudioEventEmitter useSound = null;

        #endregion

        #region Carryable Methods

        protected override void OnUse()
        {
            if (interactor.candidate == null ||
                interactor.candidate is Icicle)
            {
                useSound.Play();
            }
        }

        #endregion
    }
}