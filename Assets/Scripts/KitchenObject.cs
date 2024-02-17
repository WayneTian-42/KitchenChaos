using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    // 厨房预制体scriptable object
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    // 该object所在的counter
    private ClearCounter clearCounter;
    /// <summary>
    /// 获取prefab对应的scriptable object
    /// </summary>
    /// <returns>厨房scirpitable object</returns>
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    /// <summary>
    /// 设置物品对应的新柜台并修改逻辑以及视觉效果
    /// </summary>
    /// <param name="newClearCounter">物品放置的新柜台</param>
    public void SetClearCounter(ClearCounter newClearCounter)
    {
        // 修改柜台和物品的绑定逻辑
        // 清空当前柜台
        if (clearCounter != null)
        {
            clearCounter.ClearKitchenObject();
        }

        // 如果新柜台已经具有物品，则报错
        if (newClearCounter.HasKitchenObject())
        {
            Debug.LogError("Counter already had a kitchen object!");
        }

        // 更新柜台
        clearCounter = newClearCounter;
        // 更新柜台的物品
        clearCounter.SetKitchenObject(this);

        // 直接修改物品的父级对象，相当于同时移动在Hierarchy中的逻辑结构以及视觉效果上的物理位置
        transform.parent = clearCounter.GetCounterTopPoint();
        transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// 获取物品对应的柜台
    /// </summary>
    /// <returns>物品对应的柜台</returns>
    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }
}
