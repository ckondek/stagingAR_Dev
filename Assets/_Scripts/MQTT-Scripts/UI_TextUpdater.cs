using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_TextUpdater : MonoBehaviour
{
    public TextMeshProUGUI textTMP;
    public TextMeshProUGUI otherPlayersTMP;
    
    public void UpdateText(string text)
    {
        textTMP.text = text;
    }

    public void UpdateOtherPlayers(string op)
    {
        otherPlayersTMP.text = op;
    }
}
