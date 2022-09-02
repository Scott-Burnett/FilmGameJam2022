using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUI : MonoBehaviour
{
    public static MainUI Instance;
    private void Awake()
    {
        Instance = this;
    }
    [SerializeField] Image inspectionFade;
    [SerializeField] TextMeshProUGUI descriptionText;

    public void ShowText(string text)
    {
        descriptionText.enabled = true;
        descriptionText.text = text;
    }
    public void HideText()
    {
        descriptionText.text = "";
        descriptionText.enabled = false;
    }

    public void Fade(Color startColour, Color targetColour, float lerpTime)
    {
        StartCoroutine(FadeCoroutine(startColour, targetColour, lerpTime));
    }

    IEnumerator FadeCoroutine(Color startColour, Color targetColour, float lerpTime)
    {
        float timeElapsed = 0;
        inspectionFade.color = startColour;
        while (timeElapsed < lerpTime)
        {
            inspectionFade.color = Color.Lerp(startColour, targetColour,  (timeElapsed / lerpTime));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        inspectionFade.color = targetColour;
        yield return null;
    }
}
