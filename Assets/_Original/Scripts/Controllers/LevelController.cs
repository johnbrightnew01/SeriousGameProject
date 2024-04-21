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
    public const string LEVEL_NO_ORG = "LEVEL_NO_ORG";
    public const string LEVEL_NO_INGAME = "LEVEL_NO_INGAME";
    public const string LEVEL_COLOR_PALATE = "LEVEL_COLOR_PALATE";
    public const string FIRE_RATE_ID = "FIRE_RATE_ID";
    public const string DAMAGE_ID = "DAMAGE_ID";
    public const string LAST_SKIN_ID = "LAST_SKIN_ID";
    public const string LAST_GUN_ID = "LAST_GUN_ID";
    public const string TUTORIAL_STATE = "TUTORIAL_STATE";
    public const string GIFT_UNLOCK_COUNTER_ID = "GIFT_UNLOCK_COUNTER_ID";
 
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
    //    StoreManager.GiveProduct(StoreManager.DEFAULT_VIRTUAL_CURENCY, 500000);
        
       /* { // delete this
            for (int i = 0; i < Puller.Instance.productList.Count; i++)
            {
                if (!StoreManager.IsPurchased(Puller.Instance.productList[i].ID))
                {
                    StoreManager.GiveProduct(Puller.Instance.productList[i].ID, 1);
                }
            }
        }*/
    }

    private void Update()
    {
        if (Input.GetKeyDown("d"))
        {
            fireRateValue = fireRateCurve.Evaluate(indx);
            damageValue = damageCurve.Evaluate(indx);
        }
    }

    private void SpawnLevel()
    {
    
        isRepetedLevel = false;
        if(GetCurrentLevelNo(false) == lastLevelNo)
        {
            isRepetedLevel = true;
        }

        var tG = Instantiate(levelList[(debugLevel == -1)?GetCurrentLevelNo(true) : debugLevel]);
        currentLevelView = tG.GetComponent<LevelView>();

     //   ApplyColorPalate();
        lastLevelNo = GetCurrentLevelNo(false);
       
    }

    public bool GetIsRepetedLevel()
    {
        return isRepetedLevel;
    }


    private int GetColorPalateIndx()
    {
        if(GetCurrentLevelNo(false) % 3 == 0)
        {
            int lastPalate = PlayerPrefs.GetInt(LEVEL_COLOR_PALATE);
            lastPalate++;
            if(lastPalate >= colorPalateList.Count)
            {
                lastPalate = 0;
            }
            PlayerPrefs.SetInt(LEVEL_COLOR_PALATE, lastPalate);
        }
        return PlayerPrefs.GetInt(LEVEL_COLOR_PALATE);
    }


    public int GetCurrentLevelNo(bool isOrg)
    {
        if (isOrg)
        {
            return PlayerPrefs.GetInt(LEVEL_NO_ORG);
        }
        return PlayerPrefs.GetInt(LEVEL_NO_INGAME);
    }

 

    public void UnlockNewLevel()
    {
        int tmpLastInGameLevel = GetCurrentLevelNo(false);
        int tmpLastOrgLevel = GetCurrentLevelNo(true);
        retryCounter = 0;
        if (levelList.Count > tmpLastInGameLevel)
        {            
            PlayerPrefs.SetInt(LEVEL_NO_ORG, tmpLastOrgLevel + 1);
        }
        else
        {
            if (doRandomAfterEnd)
            {
                PlayerPrefs.SetInt(LEVEL_NO_ORG, Random.Range(0, levelList.Count));
            }
            else
            {
                PlayerPrefs.SetInt(LEVEL_NO_ORG, 0);
            }
        }
        PlayerPrefs.SetInt(LEVEL_NO_INGAME, tmpLastInGameLevel + 1);
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
        if (!isWin)
        {
            levelDifficultyController -= .1f;
            retryCounter++;
        }
        else
        {
            levelDifficultyController = 0;
            retryCounter = 0;
        
        }
        isSuccess = isWin;        
        Controller.self.gameController.OnGameOver();
        
    }





}
