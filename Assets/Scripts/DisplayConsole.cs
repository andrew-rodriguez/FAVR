using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayConsole : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI _textDisplay;


    public void DisplayText(string message)
    {
        if (_textDisplay != null)
        {
            _textDisplay.text = message;
        }
    }


    public void AppendDisplayText(string message)
    {
        if (_textDisplay != null)
        {
            _textDisplay.text += "\n" + message;
        }
    }
}
