using System.Collections;
using Hungover.Interactables.Carryables;
using UnityEngine;
using UnityEditor;

namespace Hungover.Interactables
{
    public class Door : Barricade
    {
        #region Editor Fields

        [Space(10)]
        [Header("References")]
        [SerializeField] private Key key = null;
        [SerializeField] private Transform rotationAxis = null;

        [Space(10)]
        [Header("Configuration")]
        [SerializeField] private float openAngle = 90.0f;
        [SerializeField] private float openDuration = 0.5f;

        #endregion

        #region Private Fields

        private Vector3 closedEulerAngles;
        private Vector3 openEulerAngles;
        private int unlockId;

        #endregion

        #region Barricade Methods

        protected override bool ConditionsToUnlockAreMet(Interactor interactor) =>
            interactor.curentInteractable is Key keyInHand &&
            keyInHand.id == unlockId;

        protected override void OnUnlock(Interactor interactor)
        {
            Destroy(interactor.curentInteractable.gameObject);
        }

        protected override void Open()
        {
            StopAllCoroutines();
            StartCoroutine(LerpToAngle(openEulerAngles));
        }

        protected override void Close()
        {
            StopAllCoroutines();
            StartCoroutine(LerpToAngle(closedEulerAngles));
        }

        #endregion

        #region Interactable Methods

        protected override void Initialise()
        {
            closedEulerAngles = transform.localEulerAngles;
            openEulerAngles = closedEulerAngles + rotationAxis.up * openAngle;
            unlockId = key.id;
        }

        public override void OnUpdate(){}
        public override void OnDispose(){}

        #endregion

        #region Private Methods

        private IEnumerator LerpToAngle(Vector3 targetEulerAngles)
        {
            float angleToRotate = targetEulerAngles.y - transform.localEulerAngles.y;
            float degreesPerSecond = angleToRotate / openDuration;
            float elapsedTime = 0.0f;
            while (elapsedTime < openDuration)
            {
                transform.RotateAround(rotationAxis.position, rotationAxis.up, degreesPerSecond * Time.deltaTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.localEulerAngles = targetEulerAngles;
            yield return null;
        }

        #endregion
    }
}