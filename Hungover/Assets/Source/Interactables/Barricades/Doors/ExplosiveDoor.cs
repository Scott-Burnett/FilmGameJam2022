using FMODUnity;
using Hungover;
using Hungover.Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveDoor : Barricade
{
    [SerializeField] private StudioEventEmitter explosionSound = null;

    [SerializeField] GameObject explosionEffect;
    [SerializeField] GameObject brokenDoorPrefab;
    [Space]
    [SerializeField] Transform bombPoint;
    [SerializeField] float bombMoveSpeed = 1;
    [SerializeField] float bombFuseTime = 5;


    protected override bool ConditionsToUnlockAreMet(Interactor interactor) =>
            InteractorIsHoldingC4(interactor);

    protected override void OnUnlock(Interactor interactor)
    {
        if (InteractorIsHoldingC4(interactor))
        {
            StartCoroutine(ExplosionSequence(interactor.currentInteractable.gameObject));
            interactor.currentInteractable.transform.SetParent(null);
            interactor.EndInteraction();
        }
    }

    IEnumerator ExplosionSequence(GameObject bomb)
    {
        float timeElapsed = 0;

        Vector3 currentPosition = bomb.transform.position;
        Quaternion currentRotation = bomb.transform.rotation;

        while (timeElapsed < bombMoveSpeed)
        {
            bomb.transform.position = Vector3.Lerp(currentPosition, bombPoint.position, (timeElapsed / bombMoveSpeed));
            bomb.transform.rotation = Quaternion.Lerp(currentRotation, bombPoint.rotation, (timeElapsed / bombMoveSpeed));
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        bomb.transform.position = bombPoint.position;
        bomb.transform.rotation = bombPoint.rotation;


        
        explosionSound.Play();
        yield return new WaitForSeconds(bombFuseTime);

        bomb.SetActive(false);
        Explode(bombPoint.position);

        yield return null;
    }


    void Explode(Vector3 position)
    {
        Instantiate(explosionEffect, position, Quaternion.identity);
        brokenDoorPrefab?.SetActive(true);
        gameObject.SetActive(false);
    }


    private bool InteractorIsHoldingC4(Interactor interactor) =>
           interactor.currentInteractable is C4;

}
