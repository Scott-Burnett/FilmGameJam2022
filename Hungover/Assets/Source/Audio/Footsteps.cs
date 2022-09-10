using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField] FMODUnity.StudioEventEmitter boot;
    [SerializeField] FMODUnity.StudioEventEmitter flipFlop;

    public void BootSound()
    {
        boot.Play();
    }
    public void FlipFlopSound()
    {
        flipFlop.Play();
    }

}
