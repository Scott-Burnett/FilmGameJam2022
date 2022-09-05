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

        #endregion

        #region Private Members

        protected Interactor interactor;
        protected Rigidbody thisRigidBody;

        #endregion

        #region Virtual Methods

        protected virtual void OnUse(){}

        #endregion

        #region Interactable Methods

        protected override void Initialise()
        {
            thisRigidBody = GetComponent<Rigidbody>();

            // thisRigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        }

        public override void OnInteract(Interactor interactor)
        {
            this.interactor = interactor;
            this.interactor.state = Interactor.State.Carrying;

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
            transform.parent = null;
            thisRigidBody.isKinematic = false;
        }

        public override Sprite Indicator() => 
            MainUI.Instance.handCrosshairSprite;

        #endregion

        #region Private Methods

        private IEnumerator LerpToCarryPoint(float lerpTime)
        {
            float timeElapsed = 0;

            Vector3 currentPosition = transform.position;
            Quaternion currentRotation = transform.rotation;

            Quaternion targetRotation = interactor.CarryPoint.rotation * carryingOffset;

            while (timeElapsed < lerpTime)
            {
                transform.position = Vector3.Lerp(currentPosition, interactor.CarryPoint.position, (timeElapsed / lerpTime));
                transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, (timeElapsed / lerpTime));
                timeElapsed += Time.deltaTime;

                yield return null;
            }

            transform.position = interactor.CarryPoint.position;
            transform.rotation = targetRotation;
            
            transform.parent = interactor.CarryPoint;
            
            yield return null;
        }

        private void Drop()
        {
            transform.parent = null;
            thisRigidBody.isKinematic = false;
            interactor.EndInteraction();
        }

        #endregion
    }
}