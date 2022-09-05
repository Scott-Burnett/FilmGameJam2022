using Hungover;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeInteractable : Interactable
{
    Interactor interactor;
    public override void OnDispose()
    {
        this.interactor.SetControlsEnabled(true);
        this.interactor.EndInteraction();
        this.interactor = null;
        MainUI.Instance.HideText();
        MainUI.Instance.HideKeypad();
        GetComponent<Collider>().enabled = true;
        
    }

    public override void OnInteract(Interactor interactor)
    {
        this.interactor = interactor;
        this.interactor.state = Interactor.State.Inspecting;
        this.interactor.SetControlsEnabled(false);
        
        MainUI.Instance.ShowKeypad(1234, 
                        () => 
                        {
                            print("Safe open");
                            OnDispose();
                        }, 
                        () =>
                        {
                            print("Wrong code");
                        });

        MainUI.Instance.ShowText("Enter safe code...");
        GetComponent<Collider>().enabled = false;
    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(Constants.disposeKeyCode))
        {
            OnDispose();
        }
    }

    public override Sprite Indicator() => 
        MainUI.Instance.lockCrosshairSprite;
}
