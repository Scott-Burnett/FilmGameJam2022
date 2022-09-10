using FMODUnity;
using Hungover.Interactables.Barricades.BreakableBarricades;
using System.Collections.Generic;
using UnityEngine;

namespace Hungover.Interactables.Carryables
{
    public class Blowtorch : Carryable
    {
        #region Editor Fields

        [SerializeField] private StudioEventEmitter useSound = null;
        [SerializeField] private ParticleSystem flame = null;
        [Space]
        [SerializeField] private List<GameObject> ignoreSetLayer = new List<GameObject>();

        #endregion

        #region Carryable Methods

        protected override void OnUse()
        {
            if (interactor.candidate == null ||
                interactor.candidate is Icicle)
            {
                flame?.Play();
                useSound.Play();
            }
        }

        protected override void SetLayerRecursively(int layerIndex)
        {
            gameObject.layer = layerIndex;
            foreach (Transform item in transform)
            {
                if (ignoreSetLayer.Contains(item.gameObject))
                {
                    continue;
                }
                else
                {
                    item.gameObject.layer = layerIndex;
                }
            }
        }
        #endregion
    }
}