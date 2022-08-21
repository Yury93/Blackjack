using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBlocker : MonoBehaviour
{
    public static void ButtonBlock(bool unBlock, Button but, Button but2 = null, Button but3 = null, Button but4 = null)
    {
        List<Button> buttons = new List<Button>();
        buttons?.Add(but);
        buttons?.Add(but2);
        buttons?.Add(but3);
        buttons?.Add(but4);

        foreach (var item in buttons)
        {
            if (item)
                item.interactable = unBlock;
        }

    }
    public static void ButtonSetActive(bool active, Button but, Button but2 = null, Button but3 = null, Button but4 = null)
    {
        List<Button> buttons = new List<Button>();
        buttons?.Add(but);
        buttons?.Add(but2);
        buttons?.Add(but3);
        buttons?.Add(but4);
        foreach (var item in buttons)
        {
            if (item)
                item.gameObject.SetActive(active);
        }

    }
}
