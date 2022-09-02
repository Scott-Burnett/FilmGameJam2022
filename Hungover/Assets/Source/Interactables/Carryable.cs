using UnityEngine;

namespace Hungover.Interactables
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Carryable : Interactable
    {
        #region Private Members

        private Interactor interactor;
        private Rigidbody thisRigidBody;

        #endregion

        #region Virtual Methods

        protected virtual void OnUse(){}

        #endregion

        #region Interactable Methods

        protected override void Initialise()
        {
            thisRigidBody = GetComponent<Rigidbody>();
        }

        public override void OnInteract(Interactor interactor)
        {
            this.interactor = interactor;
            this.interactor.state = Interactor.State.Carrying;

            thisRigidBody.isKinematic = true;

            transform.parent = interactor.CarryPoint;
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(Constants.interactionKeyCode))
            {
                OnUse();
            }

            if (Input.GetKeyDown(Constants.disposeKeyCode))
            {
                OnDispose();
            }
        }

        public override void OnDispose()
        {
            transform.parent = null;
            thisRigidBody.isKinematic = false;
        }

        #endregion
    }
}