using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager Instance
    {
        get; private set ;
    }
     private Plant currentPlant ;
     public  List<Plant> plantPrefabList ;

     void Awake()
     {
         Instance =this ;
     }

     void Update()
     {
         FollowCursor();
     }

     public bool AddPlant(PlantType plantType)
     {
         if (currentPlant !=null)
         {
             return  false ;
         }
         Plant plantPrefab = GetPlantPrefab( plantType) ;
         currentPlant =GameObject .Instantiate(plantPrefab) ; //初始化只能初始预制体;
         return  true    ;
     }

     public Plant  GetPlantPrefab(PlantType plantType)
     {
         foreach ( Plant plant in plantPrefabList)
         {
             if ( plant.plantType  == plantType)
             {
                  return plant ;
             }
         }
         return null ;
     }

     public void FollowCursor()
     {
         if(currentPlant!=null ) return  ;
         Vector3  mouseWorldPosition =Camera.main.ScreenToWorldPoint(Input.mousePosition);
         mouseWorldPosition.z=0 ;
         currentPlant.transform .position =  mouseWorldPosition ;

     }

     public void OnCellClick(Cell cell)
     {
         if (currentPlant== null)  return;
         bool isSuccess =cell. AddPlant(currentPlant) ;
         if (isSuccess)
         {
              currentPlant =null ;
              AudioManager .Instance .PlayClip(Config.plant);
         }


     }

}
