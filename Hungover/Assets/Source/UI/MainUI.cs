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
    [SerializeField] private GameObject mainMenuVideoPlayerGameObejct;
    [SerializeField] private StudioEventEmitter mainMenuTheme;

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
                mainMenuTheme.Stop();
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

    IEnumerator FadeOutMainMenuImageAndEnableControls()
    {
        yield return FadeMainMenuImage(new Color(0, 0, 0, 0), 6.0f);
        Destroy(mainMenuImage.gameObject);
        mainMenuImage = null;
        Destroy(mainMenuVideoPlayerGameObejct);
        mainMenuVideoPlayerGameObejct = null;
        
        interactor.SetControlsEnabled(true);
        yield return null;
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