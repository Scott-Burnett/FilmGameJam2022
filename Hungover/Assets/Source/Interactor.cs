using UnityEngine;

namespace Hungover
{
    public class Interactor : MonoBehaviour
    {
        public enum State
        {
            NotInteracting,
            Carrying,
            Inspecting
        }

        #region Constants

        private const float interactionRange = 1.0f;

        #endregion    

        #region Public Members

        public State state;

        #endregion    

        #region Private Members

        private RaycastHit hit;
        private Interactable curentInteractable;
        private Interactable candidate;

        #endregion

        #region Monobehaviour Methods

        private void Update()
        {
            switch (state)
            {
                case State.NotInteracting:
                    ScanForInteractable();
                    break;
                case State.Carrying:
                    ScanForInteractable();
                    curentInteractable.OnUpdate();
                    break;
                case State.Inspecting:
                    curentInteractable.OnUpdate();
                    break;
            }
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        private bool CameraIsLookingAtObject() =>
            Physics.Raycast(transform.position, transform.forward, out hit, interactionRange, Constants.interactableLayerMask);

        private bool ObjectIsInteractable(out Interactable interactableComponent) =>
            hit.collider.gameObject.TryGetComponent<Interactable>(out interactableComponent);

        private void ScanForInteractable()
        {
            if (CameraIsLookingAtObject() &&
                ObjectIsInteractable(out Interactable newCandidate))
            {
                if (candidate != newCandidate)
                {
                    candidate?.HideInteractableIndicator();
                    candidate = newCandidate;

                    if (candidate == null) 
                        return;

                    candidate.ShowInteractableIndicator();
                }

                if (Input.GetKeyDown(Constants.interactionKeyCode))
                {
                    curentInteractable = candidate;
                    curentInteractable.OnInteract(this);
                }
            }
        }

        #endregion
    }
}