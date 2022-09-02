using UnityEngine;

namespace Hungover
{
    public static class Constants
    {
        #region Constants

        public const int defaultLayer = 0;
        public const int defaultLayerMask = 1 << defaultLayer;
        public const int interactableLayer = 7;
        public const int interactableLayerMask = 1 << interactableLayer;
        public const int characterLayer = 8;
        public const int characterLayerMask = 1 << characterLayer;
        public const int everythingLayerMask = ~0;
        public const int inspectingLayer = 10;

        // ToDo: This shouldnt be a constant here
        public const KeyCode interactionKeyCode = KeyCode.Mouse0;
        public const KeyCode disposeKeyCode = KeyCode.Mouse1;
        public const KeyCode crouchKey = KeyCode.C;

        #endregion
    }
}