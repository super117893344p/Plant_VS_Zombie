using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardListUI : MonoBehaviour
{
    public List<Card> listCard ;
    public void ShowCardList () {
    GetComponent <RectTransform >().DOAnchorPosY (-48,1 ) ;
    EnableCardList() ;
    }

    public void EnableCardList()
    {
        foreach (Card card  in  listCard )
        {
            card.EnableCard() ;
        }
    }

    public void DisableCardList()
    {
        foreach (Card card in  listCard )
        {
            card .DisableCard();
        }
    }

     void Start()
    {
        DisableCardList( ) ;
    }



}
