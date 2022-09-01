using Hungover;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspectable : Interactable
{
    private Interactor interactor;

    private Vector3 startPosition;

    protected override void Initialise()
    {
        startPosition = transform.position;

    }
    public override void OnDispose()
    {
        this.interactor.SetControlsEnabled(true);
        this.interactor.EndInteraction();
        this.interactor = null;
        LerpPos(startPosition);
    }

    public override void OnInteract(Interactor interactor)
    {
        this.interactor = interactor;
        this.interactor.state = Interactor.State.Inspecting;
        this.interactor.SetControlsEnabled(false);
        LerpPos(interactor.InspectionPoint.position);
    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(Constants.disposeKeyCode))
        {
            OnDispose();
        }
    }

    void LerpPos(Vector3 inspectionPosition)
    {
        StartCoroutine(LerpOverTime(gameObject, inspectionPosition, 0.33f));
    }

    IEnumerator LerpOverTime(GameObject go, Vector3 destination, float lerpTime)
    {
        float timeElapsed = 0;

        Vector3 currentPosition = go.transform.position;

        while (timeElapsed < lerpTime)
        {
            transform.position = Vector3.Lerp(currentPosition, destination, (timeElapsed / lerpTime));
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = destination;
        yield return null;
    }
}
