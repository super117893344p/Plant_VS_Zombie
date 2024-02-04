using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SpawnState
{
    NotStart,
    Spawning,
    End
}
public class ZombieManager : MonoBehaviour
{
    public static ZombieManager  Instance { get ; private set ;}
    private SpawnState spawnState =SpawnState.NotStart ;
    public Transform []  spawnPointList ;
    public GameObject zombiePrefab ;
    private  List<Zombie>  zombieList ;
    void Awake( )  {Instance = this ; }

    public void Update()
    {
        if ( spawnState ==SpawnState.End&& zombieList.Count ==0)
        {
              GameManager.Instance .GameEndSuccess();
        }
    }

    public void StartSpawn()
    {
        spawnState=SpawnState.Spawning  ;
        StartCoroutine(SpawnZombie()) ;
    }

    public void Pause()
    {
        foreach ( Zombie  zombie in zombieList)
        {
              zombie.TransitionToPause();
        }  spawnState=SpawnState.End ;

    }

    IEnumerator SpawnZombie()
    {
        for (int i = 0; i<5 ; i++)
        {
             SpawnARandomZombie();
             yield return   new WaitForSeconds(3);
        }
        yield return  new WaitForSeconds(1) ;
        for (int i = 0; i < 10; i++)
        {
            SpawnARandomZombie();
            yield return new WaitForSeconds( 3) ;
        }
        yield return  new WaitForSeconds(1) ;
        AudioManager .Instance.PlayClip(Config.lastwave);
        for (int i = 0; i < 20; i++)
        {
             SpawnARandomZombie();
             yield return new WaitForSeconds(3) ;
        }
        spawnState =SpawnState.End ;
    }

    public void RemoveZombie(Zombie zombie)
    {
        zombieList.Remove(zombie ) ;
    }

    public void SpawnARandomZombie()
    {
        if (spawnState==SpawnState.Spawning )
        {
            int index = Random.Range(0, spawnPointList.Length) ;
            GameObject go =GameObject .Instantiate(zombiePrefab ,spawnPointList[index].position ,
                Quaternion.identity) ;
            zombieList.Add(go.GetComponent<Zombie>());
            go.GetComponent<SpriteRenderer >() .sortingOrder =zombieList[index].GetComponent<SpriteRenderer>()
                .sortingOrder ;
        }
    }
}
