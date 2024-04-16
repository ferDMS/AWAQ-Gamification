using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegistroDropdownControl : MonoBehaviour
{
    public Dropdown dropdown;
    string hexColor = "#323232";
    Color newColor;

    // Resetear el color
    public void ResetColor()
    {
        ColorUtility.TryParseHtmlString(hexColor, out newColor);
        dropdown.captionText.color = newColor;
    }
}
