using Hungover;
using System.Collections;
using System.Collections.Generic;
using Hungover.Interactables.Doors;
using UnityEngine;

public class SafeInteractable : Interactable
{
    #region Editor Fields

    [SerializeField]
    private SafeDoor safeDoor = null;

    #endregion

    Interactor interactor;

    protected override void Initialise()
    {
        SetLayerRecursively(Constants.doorLayer);
    }

    public override void OnDispose()
    {
        this.interactor.SetControlsEnabled(true);
        this.interactor.EndInteraction();
        this.interactor = null;
        MainUI.Instance.HideText();
        MainUI.Instance.HideKeypad();
    }

    public override void OnInteract(Interactor interactor)
    {
        this.interactor = interactor;
        this.interactor.state = Interactor.State.Inspecting;
        this.interactor.SetControlsEnabled(false);
        
        MainUI.Instance.ShowKeypad(1234, 
                        () => 
                        {
                            // Correct Code Entred
                            safeDoor.Unlock();
                            safeDoor.OnInteract(interactor);
                            SetLayerRecursively(Constants.defaultLayer);
                            OnDispose();
                        }, 
                        () =>
                        {
                            // Incorrect Code Entred
                        });

        MainUI.Instance.ShowText("Enter safe code...");
    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(Constants.disposeKeyCode))
        {
            OnDispose();
        }
    }
}
