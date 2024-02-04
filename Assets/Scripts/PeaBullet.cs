using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullet : MonoBehaviour
{
   private float speed=3  ;
   private int   atkValue=30 ;
   public GameObject  peaBulletHitPrefab ;

   public void SetAtkValue(int atkValue)
   {
      this.atkValue =atkValue ;


   }

   public void SetSpeed(int speed)
   {
      this .speed =speed ;
   }

   void Start()
   {
      Destroy( gameObject, 10 );
   }

   void Update()
   {
      transform .Translate(Vector3.right * speed*Time.deltaTime);
      // 在每一帧中，将对象沿着 x 轴正方向移动 1 个单位的距离  豌豆的发射
   }

   private void OnTriggerEnter2D(Collider2D  collision )
   {
      if (collision . tag =="Zombie")
      {
         Destroy(this.gameObject);
         collision .GetComponent<Zombie>().TakeDamage(atkValue);
         GameObject go = GameObject.Instantiate(peaBulletHitPrefab , this.transform .position,
            Quaternion.identity) ;
         Destroy(go,1);
      }
   }

}
