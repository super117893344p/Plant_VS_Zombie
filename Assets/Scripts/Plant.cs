using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

enum PlantState
{
    Disable,
    Enable
}

public class Plant : MonoBehaviour
{
   private PlantState plantState= PlantState.Disable ;
   public PlantType plantType= PlantType.Sunflower ; //Xiang ri kui
   public int HP =100 ;

   void Start()
   {
       TransitionToDisable();
   }

   void Update()
   {
       switch (plantState)
       {
           case PlantState.Disable:  DisableUpdate(); break;
           case PlantState.Enable : EnableUpdate();  break;
           default: break;
       }
   }

   public void TransitionToDisable()
   {
       plantState =PlantState.Disable ;
       GetComponent<Animator>().enabled=false;
       GetComponent< Collider2D>().enabled =false ;
   }

   public void TransitionToEnable()
   {
       plantState =PlantState.Enable ;
       GetComponent< Animator>() .enabled =true ;
       GetComponent<Collider2D >().enabled =true ;
   }

   public void TakeDamage( int damage )
   {
       HP-=damage ;
       if (HP <=0 )
       {
            Destroy(this.gameObject);
       }
   }
   void DisableUpdate()
   {

   }
   protected virtual void EnableUpdate()
   {

   }



}
