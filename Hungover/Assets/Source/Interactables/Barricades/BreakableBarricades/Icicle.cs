using Hungover.Interactables.Carryables;
using System.Collections;
using UnityEngine;

namespace Hungover.Interactables.Barricades.BreakableBarricades
{
    public class Icicle : BreakableBarrricade<Blowtorch>
    {

        [SerializeField] ParticleSystem iceBreakEffect;
        protected override void OnUnlock(Interactor interactor)
        {
            StartCoroutine(WaitBeforeDestroy());
        }

        IEnumerator WaitBeforeDestroy()
        {
            yield return new WaitForSeconds(0.25f);
            iceBreakEffect.Play();
            Destroy(this.gameObject);
        }

        public override Sprite Indicator() => 
            MainUI.Instance.handCrosshairSprite;
    }
}