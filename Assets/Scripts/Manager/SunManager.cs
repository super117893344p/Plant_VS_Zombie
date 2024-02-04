using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SunManager : MonoBehaviour
{
    public static SunManager Instance
    {
        get; private set ;
    }
    [SerializeField]
    private float sunPoint ;
    public float  SunPoint { get { return sunPoint ;}}

    void Awake()
    {
        Instance =this ;
    }    public TextMeshProUGUI sunPointText ;
    private Vector3 sunPointTextPosition ;
    public  GameObject sunPrefab ;
    public   float produceTime=3 ;
    private float produceTimer =0 ;
    private bool isStartProduce =false ;

    void Start()
    {
        UpdateSunPointText();
        CalcSunPointTextPosition();

    }

    private void Update()
    {
        if (isStartProduce)
        {
            ProduceSun();
        }
    }

    public void StartProduce()
    {
        isStartProduce =true ;
    }

    public void StopProduce()
    {
        isStartProduce =  false ;
    }


    public void UpdateSunPointText()
    {
        sunPointText.text =sunPoint.ToString() ;
    }

    public void SubSun(float sun)
    {
        this.sunPoint-=sun ;
        UpdateSunPointText();
    }

    public void AddSun(float sun)
    {
        this.sunPoint+=sun ;
        UpdateSunPointText();
    }
    public Vector2 GetSunPointTextPosition() {return sunPointTextPosition ; }

    public void CalcSunPointTextPosition()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(sunPointText.transform.position) ;
          position .z=0 ;
          sunPointTextPosition =position ;
    }

    public void ProduceSun()
    {
        produceTimer += Time.deltaTime ;
        if (produceTimer >produceTime)
        {
            produceTimer =0 ;
            Vector3 position = new Vector3(Random.Range(-5,6.5f), 6.2f ,-1) ;
            GameObject go  =  GameObject  .Instantiate( sunPrefab ,position , Quaternion.identity)  ;
            position .y=Random.Range(-4 , 3f) ;
            go.GetComponent< Sun>().LinearTo(position );
        }
    }
}
