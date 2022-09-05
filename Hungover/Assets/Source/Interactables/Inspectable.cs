using Hungover;
using System.Collections;
using UnityEngine;

public class Inspectable : Interactable
{
    [SerializeField] string description;

    private Interactor interactor;

    private Vector3 startPosition;
    private Quaternion startRotation;

    protected override void Initialise()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }
    public override void OnDispose()
    {
        this.interactor.SetControlsEnabled(true);
        this.interactor.EndInteraction();
        this.interactor = null;
        MainUI.Instance.Fade(new Color(0, 0, 0, 0.75f), Color.clear, 0.75f);
        MainUI.Instance.HideText();
        MainUI.Instance.ShowCrosshair();
        SetLayerRecursively(Constants.interactableLayer);
        LerpTo(startPosition, startRotation);
        transform.rotation = startRotation;
    }

    public override void OnInteract(Interactor interactor)
    {
        this.interactor = interactor;
        this.interactor.state = Interactor.State.Inspecting;
        this.interactor.SetControlsEnabled(false);
        MainUI.Instance.Fade(Color.clear, new Color(0, 0, 0, 0.75f), 0.75f);
        MainUI.Instance.ShowText(description);
        MainUI.Instance.HideCrosshair();
        SetLayerRecursively(Constants.inspectingLayer);
        LerpTo(interactor.InspectionPoint.position, Quaternion.identity);
    }

    public override void OnUpdate()
    {
        float xRotation = Input.GetAxis("Mouse X") * 3;
        float yRotation = Input.GetAxis("Mouse Y") * 3;
        transform.RotateAround(interactor.InspectionPoint.transform.position, interactor.InspectionPoint.transform.up, -xRotation);
        transform.RotateAround(interactor.InspectionPoint.transform.position, interactor.InspectionPoint.transform.right, yRotation);

        if (Input.GetKeyDown(Constants.disposeKeyCode))
        {
            OnDispose();
        }
    }

    void LerpTo(Vector3 inspectionPosition, Quaternion rotation)
    {
        StartCoroutine(LerpOverTime(gameObject, inspectionPosition, rotation, 0.33f));
    }

    IEnumerator LerpOverTime(GameObject go, Vector3 destination, Quaternion rotation, float lerpTime)
    {
        float timeElapsed = 0;

        Vector3 currentPosition = go.transform.position;
        Quaternion currentRotation = go.transform.rotation;

        while (timeElapsed < lerpTime)
        {
            transform.position = Vector3.Lerp(currentPosition, destination, (timeElapsed / lerpTime));
            transform.rotation = Quaternion.Lerp(currentRotation, rotation, (timeElapsed / lerpTime));
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = destination;
        yield return null;
    }

    public override Sprite Indicator() =>
        MainUI.Instance.eyeglassCrosshairSprite;
}
