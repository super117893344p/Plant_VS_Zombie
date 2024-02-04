using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peashooter : Plant
{
    public float shootDuration =2 ;
    private float shootTimer =0 ;
    public Transform shootPointTransform ;
    public PeaBullet peaBulletPrefab ;
    public int bulletSpeed =5 ;
    public int  atkValue=20 ;

    protected override void EnableUpdate()
    {
        shootTimer +=Time.deltaTime ; //每一帧的累加
        if (shootTimer >shootDuration )
        {
            shootTimer =0 ;
            Shoot() ;
        }
    }

    public void Shoot()
    {
        AudioManager.Instance .PlayClip(Config .shoot);
        PeaBullet peaBullet = GameObject .Instantiate(peaBulletPrefab , shootPointTransform.transform.position
        , Quaternion.identity ) ;//单位四元数，即一个没有旋转或旋转角度为零的四元数
        peaBullet.SetSpeed(bulletSpeed );
        peaBullet .SetAtkValue(atkValue);


    }


}
