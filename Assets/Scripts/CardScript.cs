using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    // Value of card, 2 of clubs = 2, etc
    public int value = 0;
    public Sprite backSprite;
    public int GetValueOfCard()
    {
        return value;
    }

    public void SetValue(int newValue)
    {
        value = newValue;
    }

    public string GetSpriteName()
    {
        return GetComponent<Image>().sprite.name;
    }

    public void SetSprite(Sprite newSprite)
    {
        gameObject.GetComponent<Image>().sprite = newSprite;
    }

    public void ResetCard()
    {
        Sprite back = GameObject.Find("Deck").GetComponent<DeckScript>().GetCardBack();
        gameObject.GetComponent<Image>().sprite = back;
        value = 0;
    }
    public void SetBackSpriteCard()
    {
        gameObject.GetComponent<Image>().sprite = backSprite;
    }
}
