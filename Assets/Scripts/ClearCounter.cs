using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    // 厨房预制体scriptable object
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    // 柜台顶部中心位置
    [SerializeField] private Transform counterTopPoint;
    // 柜台的物品
    private KitchenObject kitchenObject;

    // 测试用
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;

    private void Update()
    {
        if (testing && kitchenObject != null && Input.GetKeyDown(KeyCode.T))
        {
            kitchenObject.SetClearCounter(secondClearCounter);
        }
    }

    /// <summary>
    /// 交互函数，用于生成厨房中指定的object
    /// </summary>
    public void Interact()
    {
        // Debug.Log("Interact");
        // 只有当物品为空时才能交互，保证最多只存在一个物品
        if (kitchenObject == null)
        {
            // 在counterTopPoint位置复制一个scriptable object出来
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.GetObjectPrefab(), counterTopPoint);

            // 直接更新物品对应的柜台，同时实现逻辑操作以及视觉更改
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
            
            // 获取生成物体的KitchenObject类，再获取其对应的scriptable object，得到生成物体的名字
            // Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().GetObjectName());
        }
        else
        {
            Debug.Log(kitchenObject);
        }
    }

    /// <summary>
    /// 获取柜台顶部中心位置
    /// </summary>
    /// <returns>柜台顶部中心位置</returns>
    public Transform GetCounterTopPoint()
    {
        return counterTopPoint;
    }

    /// <summary>
    /// 设置柜台的物品
    /// </summary>
    /// <param name="newKitchenObject">物品</param>
    public void SetKitchenObject(KitchenObject newKitchenObject)
    {
        if (kitchenObject == null)
        {
            kitchenObject = newKitchenObject;
        }
        else
        {
            Debug.LogError("Counter already had a kitchen object!");
        }
    }

    /// <summary>
    /// 获取柜台的物品
    /// </summary>
    /// <returns>柜台的物品</returns>
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    /// <summary>
    /// 清空柜台物品
    /// </summary>
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    /// <summary>
    /// 判断柜台是否含有物品
    /// </summary>
    /// <returns>true表示含有物品，false表示不含有物品</returns>
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
