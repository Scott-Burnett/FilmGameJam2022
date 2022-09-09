using Hungover.Interactables.Carryables;
using UnityEngine;

namespace Hungover.Interactables.Barricades.BreakableBarricades
{
    public class Board : BreakableBarrricade<Crowbar>
    {
        [SerializeField] ParticleSystem breakEffect;
        protected override void Initialise()
        {
            base.Initialise();
            DisableInteractSound();
        }

        protected override void OnUnlock(Interactor interactor)
        {
            breakEffect?.Play();
            Destroy(this.gameObject);
        }
    }
}