using Hungover.Interactables.Carryables;

namespace Hungover.Interactables.Barricades.BreakableBarricades
{
    public class Board : BreakableBarrricade<Crowbar>
    {
        protected override void Initialise()
        {
            base.Initialise();
            DisableInteractSound();
        }
    }
}