using UnityEngine;

namespace Hungover
{
    public static class Constants
    {
        #region Constants

        public const int interactableLayer = 7;
        public const int interactableLayerMask = 1 << interactableLayer;

        // ToDo: This shouldnt be a constant here
        public const KeyCode interactionKeyCode = KeyCode.Mouse0;
        public const KeyCode disposeKeyCode = KeyCode.Mouse1;
        
        #endregion
    }
}