using FMODUnity;
using System.Collections;
using UnityEngine;

namespace Hungover.Interactables
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Carryable : Interactable
    {
        #region Editor Fields

        [Header("Carryable")]

        [SerializeField] private Quaternion carryingOffset = Quaternion.identity;
        [SerializeField] private float dropSoundThreshold = 0.4f;
        [SerializeField] private float startupSoundPreventionDelay = 5.0f;
        [SerializeField] private StudioEventEmitter dropSound = null;
 
        #endregion

        #region Constants

        private const int interactionLayerMask = Constants.interactableLayerMask |
                                                 Constants.doorLayerMask |
                                                 Constants.defaultLayerMask;

        #endregion

        #region Private Members

        protected Interactor interactor;
        protected Rigidbody thisRigidBody;
        private Vector3 previousPosition;

        #endregion

        #region Virtual Methods

        protected virtual void OnUse(){}

        #endregion

        #region Interactable Methods

        protected override void Initialise()
        {
            thisRigidBody = GetComponent<Rigidbody>();
            previousPosition = transform.position;
        }

        public override void OnInteract(Interactor interactor)
        {
            this.interactor = interactor;
            this.interactor.state = Interactor.State.Carrying;
            
            SetLayerRecursively(Constants.carryingLayer);

            thisRigidBody.isKinematic = true;

            StartCoroutine(LerpToCarryPoint(0.33f));
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(Constants.interactionKeyCode))
            {
                OnUse();
            }

            if (Input.GetKeyDown(Constants.disposeKeyCode))
            {
                Drop();
            }
        }

        public override void OnDispose()
        {
            CheckForWallHacks();
            transform.parent = null;
            thisRigidBody.isKinematic = false;
            
            SetLayerRecursively(Constants.interactableLayer);
        }

        public override Sprite Indicator() => 
            MainUI.Instance.handCrosshairSprite;

        #endregion

        #region Protected Methods

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (Time.time > startupSoundPreventionDelay &&
                collision.impulse.magnitude > dropSoundThreshold)
            {
                dropSound?.Play();
            }
        }

        #endregion

        #region Private Methods

        private IEnumerator LerpToCarryPoint(float lerpTime)
        {
            float timeElapsed = 0;

            Vector3 currentPosition = transform.position;
            Quaternion currentRotation = transform.rotation;

            while (timeElapsed < lerpTime)
            {
                transform.position = Vector3.Lerp(currentPosition, interactor.CarryPoint.position, (timeElapsed / lerpTime));
                transform.rotation = Quaternion.Lerp(currentRotation, interactor.CarryPoint.rotation * carryingOffset, (timeElapsed / lerpTime));
                timeElapsed += Time.deltaTime;

                yield return null;
            }

            transform.position = interactor.CarryPoint.position;
            transform.rotation = interactor.CarryPoint.rotation * carryingOffset;
            
            transform.parent = interactor.CarryPoint;
            
            yield return null;
        }

        private void Drop()
        {
            CheckForWallHacks();
            transform.parent = null;
            thisRigidBody.isKinematic = false;
            SetLayerRecursively(Constants.interactableLayer);
            interactor.EndInteraction();
        }

        private void CheckForWallHacks()
        {
            Vector3 direction = interactor.CarryPoint.position - interactor.transform.position;
            float distance = Vector3.Distance(interactor.CarryPoint.position, interactor.transform.position);

            if (Physics.Raycast(interactor.transform.position, direction, out RaycastHit hit, distance, interactionLayerMask, QueryTriggerInteraction.Ignore))
            {
                transform.position = interactor.transform.position + ((hit.point - interactor.transform.position) * 0.6f);
            }
        }

        private void OnDrawGizmos()
        {
            if (interactor == null)
            {
                return;
            }

            Vector3 direction = interactor.CarryPoint.position - interactor.transform.position;
            float distance = Vector3.Distance(interactor.CarryPoint.position, interactor.transform.position);
            Gizmos.color = Color.magenta;

            if (Physics.Raycast(interactor.transform.position, direction, out RaycastHit hit, distance, interactionLayerMask, QueryTriggerInteraction.Ignore))
            {
                Gizmos.DrawSphere(hit.point, 0.1f);
            }
        }

        #endregion
    }
}