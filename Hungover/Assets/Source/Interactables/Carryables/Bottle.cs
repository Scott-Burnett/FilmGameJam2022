using Hungover.Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : Carryable
{
    [SerializeField] GameObject shatterEffect;
    [SerializeField] GameObject objectInside = null;

    [SerializeField] float breakThreshold = 2;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("LevelBlockout"))
        {
            if (collision.impulse.magnitude > breakThreshold)
            {
                BreakBottle();
            }
        }
    }

    private void BreakBottle()
    {
        Instantiate(shatterEffect, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

}
