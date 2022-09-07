using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using Hungover;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MainUI : MonoBehaviour
{
    public static MainUI Instance;
    private void Awake()
    {
        Instance = this;
    }
    [SerializeField] Image inspectionFade;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] SafeKeypad keypad;
    [SerializeField] Image crosshair;

    [Header("Crosshairs")]
    [SerializeField] public Sprite crosshairSprite;
    [SerializeField] public Sprite eyeglassCrosshairSprite;
    [SerializeField] public Sprite handCrosshairSprite;
    [SerializeField] public Sprite lockCrosshairSprite;
    [SerializeField] public Sprite unlockCrosshairSprite;

    [Header("Main Menu")]
    [SerializeField] private RawImage mainMenuImage;
    // [SerializeField] private GameObject mainMenuVideoPlayerGameObejct;
    [SerializeField] private GameObject mainMenuGameObejct;
    [SerializeField] private StudioEventEmitter mainMenuTheme;
    [SerializeField, Range(0.0f, 1.0f)] private float controlGainDelayFactor = 0.5f;

    [Header("Credits")]
    [SerializeField] private GameObject creditsGameObejct = null;
    [SerializeField] private Image creditsTint;
    [SerializeField] private RectTransform creditsText;
    [SerializeField] private StudioEventEmitter creditsTheme;

    [Header("Player")]
    [SerializeField] public Interactor interactor;

    private bool inMainMenu = true;

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

    public void ShowKeypad(int codeRequired, Action success, Action failure)
    {
        keypad.Show(codeRequired, success, failure);
    }
    public void HideKeypad()
    {
        keypad.Hide();
    }

    public void Fade(Color startColour, Color targetColour, float lerpTime)
    {
        StartCoroutine(FadeCoroutine(startColour, targetColour, lerpTime));
    }

    public void FadeOutMainMenuImage()
    {
        StartCoroutine(FadeMainMenuImage(new Color(0, 0, 0, 0), 6.0f));
    }

    public void RollCredits()
    {
        StartCoroutine(DisableControlsAndFadeInMainMenuImage());
    }

    private void Start()
    {
        inMainMenu = true;
        interactor.SetControlsEnabled(false);
    }

    private void Update()
    {
        if (inMainMenu)
        {
            if (Input.anyKey)
            {
                inMainMenu = false;
                StartCoroutine(FadeOutMainMenuImageAndEnableControls());
            }
        }
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

    IEnumerator FadeMainMenuImage(Color targetColor, float duration)
    {
        Color difference = targetColor - mainMenuImage.color;
        Color colorRate = difference / duration;

        for (float elapsedTime = 0.0f; elapsedTime < duration; elapsedTime += Time.deltaTime)
        {
            mainMenuImage.color += colorRate * Time.deltaTime;
            yield return null;
        }
        mainMenuImage.color = targetColor;
        yield return null;
    }

    IEnumerator FadeMainMenuImageAndEnableControlsAfterDelay(Color targetColor, float duration)
    {
        Color difference = targetColor - mainMenuImage.color;
        Color colorRate = difference / duration;
        float controlEnableTime = duration * controlGainDelayFactor;
        bool controlsHaveBeenEnabled = false;

        for (float elapsedTime = 0.0f; elapsedTime < duration; elapsedTime += Time.deltaTime)
        {
            mainMenuImage.color += colorRate * Time.deltaTime;
            if (!controlsHaveBeenEnabled &&
                elapsedTime > controlEnableTime)
            {
                interactor.SetControlsEnabled(true);
            }

            yield return null;
        }
        mainMenuImage.color = targetColor;
        yield return null;
    }

    IEnumerator FadeInCreditsTint(Color targetColor, float duration)
    {
        Color difference = targetColor - creditsTint.color;
        Color colorRate = difference / duration;
        for (float elapsedTime = 0.0f; elapsedTime < duration; elapsedTime += Time.deltaTime)
        {
            creditsTint.color += colorRate * Time.deltaTime;
            yield return null;
        }
        creditsTint.color = targetColor;
        yield return null;
    }

    IEnumerator ScrollCreditsText(Vector3 startPosition, Vector3 endPosition, float duration)
    {
        creditsText.localPosition = startPosition;
        Vector3 distance = endPosition - startPosition;
        Vector3 rate = distance / duration;

        for (float elapsedTime = 0.0f; elapsedTime < duration; elapsedTime += Time.deltaTime)
        {
            creditsText.localPosition += rate * Time.deltaTime;
            yield return null;
        }
        creditsText.localPosition = endPosition;
        yield return null;
    }

    IEnumerator FadeOutMainMenuImageAndEnableControls()
    {
        yield return FadeMainMenuImageAndEnableControlsAfterDelay(new Color(0, 0, 0, 0), 6.0f);
        // interactor.SetControlsEnabled(true);
        mainMenuGameObejct.SetActive(false);
        mainMenuTheme.Stop();
        yield return null;
    }

    IEnumerator DisableControlsAndFadeInMainMenuImage()
    {
        interactor.SetControlsEnabled(false);
        mainMenuGameObejct.SetActive(true);
        creditsTheme.Play();
        yield return FadeCoroutine(new Color(0, 0, 0, 0), new Color(255, 255, 255, 1.0f), 2.0f);
        yield return FadeMainMenuImage(new Color(255, 255, 255, 1.0f), 4.0f);
        creditsGameObejct.SetActive(true);
        yield return FadeInCreditsTint(new Color(0, 0, 0, 0.95f), 1.0f);
        yield return ScrollCreditsText(new Vector3(0.0f, -1169.3f, 0.0f), new Vector3(0.0f, 2000.0f, 0.0f), 40.0f);
    }

    public void ShowDefaultCrosshair()
    {
        crosshair.sprite = crosshairSprite;
    }

    public void ShowInteractableIndicator(Sprite indicator)
    {
        crosshair.sprite = indicator;
    }

    public void HideCrosshair()
    {
        crosshair.enabled = false;
    }

    public void ShowCrosshair()
    {
        crosshair.enabled = true;
    }
}