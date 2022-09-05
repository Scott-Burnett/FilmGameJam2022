using Hungover;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingPot : Interactable
{
    [SerializeField] GameObject c4Prefab;
    [Header("Positions")]
    [SerializeField] Transform dropPoint;
    [SerializeField] Transform cookPoint;
    bool plastic, chemical, gunpowder;
    public override void OnInteract(Interactor interactor)
    {
        if (interactor.curentInteractable == null)
        {
            return;
        }

        switch (interactor.curentInteractable.tag)
        {
            case Constants.plasticTag:
                {
                    plastic = true;
                    DropIntoPot(interactor);
                    break;
                }
            case Constants.chemicalTag:
                {
                    chemical = true;
                    DropIntoPot(interactor);
                    break;
                }
            case Constants.gunpowderTag:
                {
                    gunpowder = true;
                    DropIntoPot(interactor);
                    break;
                }
        }
    }
    void DropIntoPot(Interactor interactor )
    {
        StartCoroutine(DropCoroutine(interactor.curentInteractable.gameObject));
        interactor.EndInteraction();
    }

    IEnumerator DropCoroutine(GameObject item)
    {
        float lerpTime = 1;
        float timeElapsed = 0;

        Vector3 currentPosition = item.transform.position;
        Quaternion currentRotation = item.transform.rotation;

        while (timeElapsed < lerpTime)
        {
            item.transform.position = Vector3.Lerp(currentPosition, dropPoint.position, (timeElapsed / lerpTime));
            item.transform.rotation = Quaternion.Lerp(currentRotation, dropPoint.rotation, (timeElapsed / lerpTime));
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        item.transform.position = dropPoint.position;
        item.transform.rotation = dropPoint.rotation;

        item.GetComponent<Rigidbody>().isKinematic = false;
        item.transform.parent = null;

        yield return new WaitForSeconds(0.25f);

        Destroy(item);

        CheckIngredients();

        yield return null;
    }

    void CheckIngredients()
    {
        if (plastic && chemical && gunpowder)
        {
            var c4 = Instantiate(c4Prefab, cookPoint.position, Quaternion.identity);
            c4.GetComponent<Rigidbody>().AddForce(cookPoint.up + cookPoint.forward * 2, ForceMode.Impulse);
        }
    }

    public override Sprite Indicator() => 
        MainUI.Instance.handCrosshairSprite;
}
