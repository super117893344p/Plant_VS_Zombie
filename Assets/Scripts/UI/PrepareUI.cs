using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareUI : MonoBehaviour
{
    private Animator animator ;
    private Action OnComplete ;

    void Start()
    {
        animator = GetComponent<Animator > () ;
        animator. enabled =false ;
    }

    public void Show(Action  onComplete) //传递一个行为委托
    {
        this.OnComplete =onComplete  ;
            animator .enabled =true ;
    }

    public void OnShowComplete()
    {
        OnComplete ?.Invoke() ;
    }
}
