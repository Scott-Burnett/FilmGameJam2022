using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using StarterAssets;

public class SafeKeypad : MonoBehaviour
{
    Action OnCodeSuccess;
    Action OnCodeFailure;

    [SerializeField] private TextMeshProUGUI inputText;
    [SerializeField] private GameObject content;
    [SerializeField] private FirstPersonController controls;

    int code; 
    string input;
    int indexOfInput = 0;

    private void OnEnable()
    {
        Reset();
    }

    public void Show(int requiredCode, Action success = null, Action failure = null)
    {
        Reset();

        code = requiredCode;
        content.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        OnCodeSuccess += success;
        OnCodeFailure += failure;
    }
    public void Hide()
    {
        content.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Reset();
        OnCodeFailure = null;
        OnCodeSuccess = null;
    }

    public void AddNumber(int newNumber)
    {
        char[] ch = input.ToCharArray();

        string num = newNumber.ToString();

        ch[indexOfInput] = (char)num[0];

        indexOfInput++;

        input = new string(ch);

        if (indexOfInput >= 4)
        {
            CheckInput(Int32.Parse(input));
        }

        SetText(input);
    }

    public void DeleteNumber()
    {
        if (indexOfInput <= 0) return;

        char[] ch = input.ToCharArray();

        string _ = "_";

        ch[indexOfInput - 1] = _[0];

        indexOfInput--;


        input = new string(ch);

        SetText(input);
    }

    void CheckInput(int inputCode)
    {
        if (inputCode == this.code)
        {
            OnCodeSuccess?.Invoke();
            Hide();
        }
        else
        {
            CodeFailCheck();
        }
    }

    void SetText(string newString)
    {
        inputText.text = newString;
    }

    void CodeFailCheck()
    {
        indexOfInput = 0;

        input = "____";

        SetText(input);

        CodeFailed();
    }

    public void CodeFailed()
    {
        OnCodeFailure?.Invoke();
    }

    void Reset()
    {
        input = "____";
        SetText(input);
        indexOfInput = 0;
        code = 0;
    }
}
