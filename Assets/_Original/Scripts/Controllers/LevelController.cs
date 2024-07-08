using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ColorPalate
{
    public Material groundMat;
    public Material fogMat;
    public Material pillerMat;   
}

public class LevelController : MonoBehaviour
{ 

 
    public List<ColorPalate> colorPalateList;
    public int colorPalateDebug = -1;
    public LevelView currentLevelView;
    [field: ReadOnly]
    [field: SerializeField]
    public bool isGamePlayStart { get; private set; }
    public Material enemyMat;
    public List<GameObject> levelList;
    [ReadOnly]
    [SerializeField]
    private bool doRandomAfterEnd = true;
    [ReadOnly]
    [SerializeField]
    private bool isSuccess;    
    public int debugLevel = -1;
    [field: ReadOnly]
    [field: SerializeField]
    public int currentLevelIncome { get; private set; }
    public Material positivePowerMat;
    public Material negativePowerMat;
    [Space]
    private static int lastLevelNo = 0;
    private static bool isRepetedLevel = false;
    
    [field: SerializeField, Space, Space]
    public int maxNumOfUpgradeForFireRate { get; private set; }
    [field: SerializeField, ReadOnly] public bool isGameOver { get; private set; } = false;

    [field: SerializeField]
    public int maxNumfoUpgradeForBulletDamage { get; private set; }


    public static int retryCounter = 0;
    public AnimationCurve fireRateCurve;
    public AnimationCurve damageCurve;
    public int indx;
    public float fireRateValue;
    public float damageValue;
    public static float levelDifficultyController = 0;

    private void Awake()
    {
     
      
    }

    private void Start()
    {
        currentLevelIncome = 0; 
    }

 
  






   

 



    public void UpdateIncome(int updateWith = 1)
    {
        currentLevelIncome += updateWith;
    }

    public bool IsLevelSuccess()
    {
        return isSuccess;
    }


    public void DoGameOver(bool isWin)
    {
        isGameOver = true;
        if (!isWin)
        {
        
        }
        else
        {
         
        
        }
        isSuccess = isWin;        
        Controller.self.gameController.OnGameOver();
        
    }





}
