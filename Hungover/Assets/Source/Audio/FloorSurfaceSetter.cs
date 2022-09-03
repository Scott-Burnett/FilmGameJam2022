using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSurfaceSetter : MonoBehaviour
{
    public LayerMask floorSurfaceTriggers;
    void Update()
    {
        if(Time.frameCount % 25 == 0)
        {
            CheckFloorSurface();
        }
        
    }

    void CheckFloorSurface()
    {
        if (Physics.Raycast(transform.position, -transform.up, out var hit, 10, floorSurfaceTriggers))
        {
            hit.collider.gameObject.GetComponent<FMODUnity.StudioGlobalParameterTrigger>()?.TriggerParameters();
        }
    }
}
