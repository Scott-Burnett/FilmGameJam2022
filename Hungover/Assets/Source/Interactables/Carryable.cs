using System.Collections;
using UnityEngine;

namespace Hungover.Interactables
{
    public abstract class Carryable : Interactable
    {
        #region Constants

        private const float groundCheckRadius = 0.1f;

        #endregion

        #region Private Members

        private Interactor interactor;

        #endregion

        #region Virtual Methods

        protected virtual void OnUse(){}

        #endregion

        #region Interactable Methods

        public override void OnInteract(Interactor interactor)
        {
            this.interactor = interactor;
            this.interactor.state = Interactor.State.Carrying;

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
            StartCoroutine(FallToGround());
        }

        #endregion

        #region Private Methods

        private bool Grounded =>
            Physics.CheckSphere(transform.position, groundCheckRadius);

        private IEnumerator FallToGround()
        {
            Vector3 velocity = Vector3.zero;
            while (!Grounded)
            {
                velocity += Physics.gravity * Time.deltaTime;
                transform.position -= velocity;
                yield return null;
            }
        }

        #endregion
    }
}