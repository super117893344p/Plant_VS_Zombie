using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance //实例化对象必要静态
    {
        get ;private set ;
    }
    public PrepareUI  prepareUI ;
    public CardListUI  cardListUI;
    public FailUI  failUI ;
    public   WinUI winUI ;
    public bool isGameEnd=false  ;

    void Awake()
    {
         Instance =this ;
    }

    void  Start()
    {
        GameStart ( ) ;
    }

    public void GameStart()
    {
        Vector3 currentPosition = Camera.main.transform.position ;
         Camera .main .transform.DOPath(
             new Vector3 [] {currentPosition , new Vector3(5,0, -10),currentPosition } ,3,PathType.Linear )
             .OnComplete(OnCameraMoveComplete);
    }

    public void GameEndFail()
    {
         if( isGameEnd )  return ;
         isGameEnd =false ;
         failUI.Show() ;
         ZombieManager.Instance.Pause() ;
         cardListUI. DisableCardList(); // 非静态成员属性必须通过对象调用 小心注意不要把对象写成了类
         SunManager .Instance.StopProduce() ;
         AudioManager.Instance.PlayClip (Config.lose_music) ;

    }
        public void GameEndSuccess() {
         if (isGameEnd == true) return;
        isGameEnd = true;
        winUI.Show();
        cardListUI.DisableCardList();
        SunManager.Instance.StopProduce();

        AudioManager.Instance.PlayClip(Config.win_music);


        }
        void OnCameraMoveComplete() {
             prepareUI.Show(OnPrepareUIComplete) ;
         }
         void OnPrepareUIComplete () {
         SunManager .Instance.StartProduce() ;
         ZombieManager.Instance.StartSpawn () ;
         cardListUI.ShowCardList() ;
         AudioManager.Instance.PlayClip(Config.bgm1) ;

         }

}

