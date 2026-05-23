using TMPro;
using UnityEngine;

public class ButtonTextChanger : MonoBehaviour
{
    public TextMeshProUGUI targetText;

    public void ChangeText()
    {
        targetText.text = "Button Clicked!";
    }
}