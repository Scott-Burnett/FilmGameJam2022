using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeTest : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MainUI.Instance.ShowKeypad(1234, () => print("Success"), () => print("Failure"));
        }
      
    }
}
