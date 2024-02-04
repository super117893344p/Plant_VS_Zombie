using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailUI : MonoBehaviour
{

        private Animator animator ;

        void Awake()
        {
                animator=GetComponent<Animator>() ;
        }

        void Start()
        {
                Hide();
        }

        public void Hide()
        {
                animator .enabled=false;
        }

        public void Show()
        {
                animator .enabled =true ;
        }
}
