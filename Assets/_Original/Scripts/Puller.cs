using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-100)]
[System.Serializable]
public class PullInfo
{
    public GameObject orgObj;
    public List<GameObject> objList = new List<GameObject>();
}

[System.Serializable]
public class productInfo
{
    public string ID;
    public Sprite icon;
    public GameObject productObj;
}

public class Puller : MonoBehaviour
{
    public static Puller Instance;
    public GameObject deadCoinParticle;
    public List< GameObject> baseBulletList;
    public List<GameObject> bulletVarientList;
    public List<PullInfo> puller = new List<PullInfo>(); 
    public List<productInfo> productList;
    private List<GameObject> randomGunList = new List<GameObject>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public GameObject GetThisObject(GameObject refObj, bool isRetunAsEnabled = false)
    {        
        for (int i = 0; i < puller.Count; i++)
        {            
            if(puller[i].orgObj.CompareTag(refObj.tag))
            {               
                if(puller[i].objList.Count >= 1)
                {
                   GameObject tmpObj = puller[i].objList[0];
                    puller[i].objList.RemoveAt(0);
                    tmpObj.SetActive(isRetunAsEnabled);
                    return tmpObj;
                }
                else
                {                  

                    GameObject tmpObj = Instantiate(puller[i].orgObj);
                    tmpObj.SetActive(isRetunAsEnabled);
                    return tmpObj;
                }
            }
        }
        GameObject newObj = Instantiate(refObj);
        newObj.SetActive(false);
        PullInfo tmpInfo = new PullInfo();
        tmpInfo.orgObj = newObj;
        puller.Add(tmpInfo);
        newObj = Instantiate(tmpInfo.orgObj);
        newObj.SetActive(isRetunAsEnabled);
        return newObj;
    }

    public GameObject GetThisObject(GameObject refObj, Vector3 returnPos, bool isRetunAsEnabled = false)
    {
        for (int i = 0; i < puller.Count; i++)
        {
            if (puller[i].orgObj.CompareTag(refObj.tag))
            {
                if (puller[i].objList.Count >= 1)
                {
                    GameObject tmpObj = puller[i].objList[0];
                    puller[i].objList.RemoveAt(0);
                    tmpObj.transform.position = returnPos;
                    tmpObj.SetActive(isRetunAsEnabled);
                    return tmpObj;
                }
                else
                {

                    GameObject tmpObj = Instantiate(puller[i].orgObj);
                    tmpObj.transform.position = returnPos;
                    tmpObj.SetActive(isRetunAsEnabled);
                    return tmpObj;
                }
            }
        }
        GameObject newObj = Instantiate(refObj);
        newObj.SetActive(false);
        PullInfo tmpInfo = new PullInfo();
        tmpInfo.orgObj = newObj;
        puller.Add(tmpInfo);
        newObj = Instantiate(tmpInfo.orgObj);
        newObj.SetActive(isRetunAsEnabled);
        newObj.transform.position = returnPos;
        return newObj;
    }

    public void RestockToPull(GameObject restockObj, float watiTime = -1f)
    {
        if (watiTime <= 0)
        {
            for (int i = 0; i < puller.Count; i++)
            {
                if (puller[i].orgObj.CompareTag(restockObj.tag))
                {
                    if(restockObj == null)
                    {
                        return;
                    }
                    restockObj.SetActive(false);
                  //  restockObj.layer = 0;
                    puller[i].objList.Add(restockObj);
                    restockObj.transform.parent = this.transform;
                }
            }
        }
        else
        {
           StartCoroutine( WaitAndRestock(restockObj, watiTime));
           
        }
    }

    IEnumerator  WaitAndRestock(GameObject restockObj, float waitime)
    {
        /*restockObj.layer = 12;
        float timeElaps = 0;
        while(timeElaps <= waitime)
        {
            if(restockObj.layer != 12)
            {
                yield break;
            }
            yield return null;
        }        
        if(restockObj.layer == 12)
        {
            restockObj.layer = 0;
            RestockToPull(restockObj);
        } */
       
        yield return new WaitForSeconds(waitime);
        if (restockObj == null)
        {
            yield break;
        }
        RestockToPull(restockObj);
    }

    public Sprite GetThisProductIcon(string id)
    {
        for (int i = 0; i < productList.Count; i++)
        {
            if(productList[i].ID == id)
            {
                return productList[i].icon;
            }
        }
        return null;
    }

    private void AddRandomGunObject()
    {
        string lastGun = PlayerPrefs.GetString("LAST_GUN_ID");
        for (int i = 9; i < productList.Count; i++)
        {
            if (productList[i].ID != lastGun)
            {
                randomGunList.Add(productList[i].productObj);
            }
        }
    }

    public GameObject GetRandomGun()
    {
        if(randomGunList.Count == 0)
        {
            AddRandomGunObject();
        }
        int rnd = Random.Range(0, randomGunList.Count);
        var objj = randomGunList[rnd];
        randomGunList.RemoveAt(rnd);
        return objj;
    }


    public GameObject GetThisProduct(string id)
    {
       
        for (int i = 0; i < productList.Count; i++)
        {
            if(productList[i].ID == id)
            {
                return productList[i].productObj;
            }
        }
        return null;
    }

 


}
