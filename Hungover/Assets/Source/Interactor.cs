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

        private const float interactionRange = 2.0f;
        private const int interactionLayerMask = Constants.interactableLayerMask |
                                                 Constants.doorLayerMask |
                                                 Constants.defaultLayerMask;

        #endregion    

        #region Public Members

        [HideInInspector] public State state;

        public Transform InspectionPoint 
        {
            get 
            {
                return inspectionPoint; 
            }
        }

        public Transform CarryPoint => carryPoint;
        public StarterAssets.FirstPersonController Controller => firstPersonControls;
        public Interactable currentInteractable { get; set; }
        public Interactable candidate { get; private set; }

        #endregion    

        #region Private Members

        private RaycastHit hit;

        [SerializeField] private Transform inspectionPoint;
        [SerializeField] private Transform carryPoint;
        [SerializeField] private StarterAssets.FirstPersonController firstPersonControls;

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
                    currentInteractable.OnUpdate();
                    ScanForInteractable();
                    break;
                case State.Inspecting:
                    currentInteractable.OnUpdate();
                    break;
            }
        }

        #endregion

        #region Public Methods

        public void SetControlsEnabled(bool value)
        {
            firstPersonControls.enabled = value;
        }

        public void EndInteraction()
        {
            state = State.NotInteracting;
            currentInteractable = null;
        }

        #endregion

        #region Private Methods

        private bool CameraIsLookingAtObject() =>
            Physics.Raycast(transform.position, transform.forward, out hit, interactionRange, interactionLayerMask, QueryTriggerInteraction.Ignore);

        private bool ObjectIsInteractable(out Interactable interactableComponent) =>
            hit.collider.gameObject.TryGetComponent<Interactable>(out interactableComponent);

        private void ScanForInteractable()
        {
            if (CameraIsLookingAtObject() &&
                ObjectIsInteractable(out Interactable newCandidate))
            {
                if (candidate != newCandidate)
                {
                    candidate = newCandidate;
                    candidate.ShowInteractableIndicator();
                }
                
                if (Input.GetKeyDown(Constants.interactionKeyCode))
                {
                    candidate.OnInteract(this);
                    candidate.PlayInteractableSound();

                    if (candidate.CausesDisposeCurrentInteractable())
                    {
                        currentInteractable?.OnDispose();
                        currentInteractable = candidate;
                    }
                }
            }
            else
            {
                MainUI.Instance.ShowDefaultCrosshair();
                candidate = null;
            }
        }

        #endregion
    }
}