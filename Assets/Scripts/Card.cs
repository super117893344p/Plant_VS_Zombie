using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum CardState
{
    Disable,
    Cooling,
    WaitingSun,
    Ready
}

public enum PlantType
{
    Sunflower,
    PeaShooter
}

public class Card : MonoBehaviour
{
         private CardState cardState =CardState.Disable ;
         private PlantType plantType =PlantType.Sunflower ;
         public GameObject cardLight ;
         public GameObject cardGray ;
         public Image cardMask ;
         private float cdTime =5;
         private float cdTimer =0 ;
         private float needSunPoint =50 ;

         void Update()
         {
             switch (cardState)
             {
                 case CardState.Cooling: CoolingUpdate(); break;
                 case CardState.WaitingSun : WaitingSunUpdate(); break;
                 case CardState.Ready : ReadyUpdate(); break;
                 default: break;

             }
         }

         void CoolingUpdate()
         {
              cdTimer+= Time.deltaTime ;
              cardMask .fillAmount =(cdTime-cdTimer) /cdTime ;
              if (cdTimer >= cdTime)
              {
                  TransitionToWaitingSun();
              }
         }

         void WaitingSunUpdate()
         {
             if (needSunPoint <= SunManager.Instance.SunPoint)
             {
                 TransitionToReady();
             }
            }

         void ReadyUpdate()
         {
             if (needSunPoint > SunManager.Instance .SunPoint )
             {
                 TransitionToWaitingSun();
             }
         }

         void TransitionToWaitingSun()
         {
             cardState =CardState.WaitingSun ;
             cardLight .SetActive(false );
             cardGray .SetActive( true);
             cardMask. gameObject.SetActive(false);

         }

         void TransitionToReady()
         {
             cardState = CardState.Ready ;
             cardLight .SetActive(true);
             cardGray .SetActive(false);
             cardMask.gameObject .SetActive(false);
         }

         void TransitionToCooling()
         {
             cardState =CardState.Cooling ;
             cdTimer =0 ;
             cardGray.SetActive(true );
             cardLight .SetActive(false);
             cardMask .gameObject .SetActive( true );
         }

         public void DisableCard()
         {
             cardState=CardState.Disable  ;
         }

         public void EnableCard()
         {
               TransitionToCooling();
         }

         public void OnClick()
         {
              AudioManager .Instance. PlayClip(Config.btn_click);
              if ( SunManager .Instance.SunPoint<= needSunPoint)
              {
                  return ;
              }

              if (cardState==CardState.Disable)
              {
            return  ;
              }
                bool  isSuccess = HandManager .Instance .AddPlant(plantType) ;
                if (isSuccess)
                {
                     SunManager.Instance.SubSun( needSunPoint); TransitionToCooling();
                }





         }
}
