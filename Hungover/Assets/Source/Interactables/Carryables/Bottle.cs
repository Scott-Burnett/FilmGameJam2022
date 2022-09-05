using Hungover.Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : Carryable
{
    [SerializeField] GameObject shatterEffect;
    [SerializeField] GameObject objectInside = null;

    [SerializeField] float breakThreshold = 2;
    [SerializeField] private Vector3 spawnOffset = new Vector3(0.0f, 0.1f, 0.0f);


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

        if (objectInside != null)
        {
            Instantiate(objectInside, transform.position + spawnOffset, transform.rotation);
        }
    }
}