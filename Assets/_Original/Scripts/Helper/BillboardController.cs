using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardController : MonoBehaviour
{
    private Camera mCam;
    [SerializeField]
    private GameObject envHolder;
    [SerializeField,ReadOnly]
    private List<BillboardView> billboardList = new List<BillboardView>();

    private void Awake()
    {
        mCam = Camera.main;
        
        var bill = envHolder.GetComponentsInChildren<BillboardView>();
        foreach (var item in bill)
        {
            billboardList.Add(item);
        }
    }

    public void AddThisToBillboard(BillboardView BilView) => billboardList.Add(BilView);

    private void LateUpdate()
    {
        for (int i = 0; i < billboardList.Count; i++)
        {
            if (billboardList[i] != null)
            {
                var rot = Quaternion.LookRotation(billboardList[i].transform.position - mCam.transform.position);

                billboardList[i].transform.rotation = Quaternion.Euler(billboardList[i].transform.rotation.eulerAngles.x,                    
                    rot.eulerAngles.y,
                    billboardList[i].transform.rotation.eulerAngles.z);
            }
        }
    }

}
