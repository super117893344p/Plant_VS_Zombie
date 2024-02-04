using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


enum ZombieState
{
    Move,
    Eat,
    Die,
    Pause
}
public class Zombie : MonoBehaviour
{
    ZombieState zombieState =ZombieState.Move  ;
     private Rigidbody2D   rgd ;
     public float  moveSpeed =2 ;
     private Animator animator ;
     public  int atkValue =30;
     public  float atkDuration =2 ;
     public float atkTimer=0 ;
     private Plant currentEatPlant ;
     public float  HP ;
     private float  currentHP ;
     public  GameObject zombieHeadPrefab ;
     private bool haveHead =true ;

     void Start()
     {
          rgd=GetComponent<Rigidbody2D >();
          animator = GetComponent<Animator >() ;
          currentHP =HP  ;
     }

     void Update()
     {
         switch (zombieState)
         {
             case ZombieState.Die:  Destroy (this.gameObject) ; break;
             case ZombieState.Move : MoveUpdate(); break;
             case ZombieState.Eat : EatUpdate(); break;
             case ZombieState.Pause : break;
             default: break;
         }
     }

     public void MoveUpdate()
     {
        rgd .MovePosition(rgd.position + Vector2.left*moveSpeed *Time.deltaTime);
     }

     public void EatUpdate()
     {
         atkTimer+=Time.deltaTime ;
         if (currentEatPlant!=null && atkTimer> atkDuration)
         {
             AudioManager.Instance .PlayClip(Config.eat);
             currentEatPlant.TakeDamage(atkValue);
             atkTimer =0 ;

         }
     }

     private void OnTriggerEnter2D(Collider2D collision )
     {
         if (collision .tag=="Plant")
         {
                animator.SetBool("IsAttacking",true ) ;
                TransitionToEat();
                currentEatPlant =collision.GetComponent<Plant>();
         }

         if (collision.tag=="House")
         {
             GameManager.Instance.GameEndFail();
         }
     }

     private void OnTriggetExit2D(Collider2D collision)
     {
         if (collision.tag=="Plant")
         {
             animator.SetBool("IsAttacking",false);
             zombieState =ZombieState.Move ;
             currentEatPlant =null ;
         }

     }
     public void TransitionToEat()
     {
         zombieState = ZombieState.Eat;
         atkTimer = 0;
     }
     public void TransitionToPause()
     {
         zombieState = ZombieState.Pause;
         animator .enabled = false;
     }

     public void TakeDamage(int damage )
     {
         if (currentHP <=0 )
         {
             Dead(); return ;
         }
         currentHP-=damage;
         float hpPercent = currentHP*1f /HP ;
         animator .SetFloat("HPPercent ", hpPercent) ;
         if (hpPercent<0.5f && haveHead)
         {
             haveHead =false ;
             GameObject go =GameObject.Instantiate(zombieHeadPrefab,this.transform.position , Quaternion.identity );
             Destroy( go,2);
             }
     }

     public void Dead()
     {
         if (zombieState==ZombieState.Die )
         {
              return  ;
         }
         zombieState =ZombieState.Die ;
         GetComponent<Collider2D>().enabled =false ;
         ZombieManager .Instance.RemoveZombie(this);
         Destroy(this.gameObject,2 );
     }





}
