using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public List<GameObject> cards;
    public float offsetX;
    public Transform start, gameFiald, finish;
    public void AddCard(CardScript cardScript)
    {
       //var lastCard =  cards.Where(c => c.GetComponent<CardScript>().stateCard == CardScript.State.open).LastOrDefault();
        
        //if(lastCard!= null)
        //{
        //    var newPosX = new Vector2(lastCard.transform.GetComponent<RectTransform>().anchoredPosition.x - offsetX, lastCard.transform.position.y);
        //    cardScript.transform.DOMove(newPosX, 2);
        //}
        //else
        //{
        //    cardScript.transform.DOMove(gameFiald.position, 2);
        //}
    }
}
